using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EzilineApp.Api.CoreApplication.Data;
using EzilineApp.Api.CoreApplication.DataTransferObjs.ReviewDetails;
using EzilineApp.Api.CoreApplication.DataTransferObjs.users_dtos;
using EzilineApp.Api.CoreApplication.IRespositories;
using EzilineApp.Api.CoreApplication.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace EzilineApp.Api.CoreApplication.Repositories 
{
     public class managereviews : Generic<ratings>, Imanagereviews 
     {
          private readonly DataContext _dataContext;
          private IMapper _mapper;

          private IHubContext<BroadCast> _hubcontext;

          public dynamic ratinginfo;
          public managereviews (DataContext dataContext) : base (dataContext) 
          {

               _dataContext = dataContext;
          }
          public void GetMapper (IMapper mapper) 
          {
               _mapper = mapper;
          }

          public void Inithub(IHubContext<BroadCast> hubContext)
          {
               _hubcontext=hubContext;
          }
          public async Task<object> Getsummary (int id) 
          {
               
               var record = await _dataContext.ratings
                                              .FirstOrDefaultAsync (x => x.webid == id);

               if (record != null) 
               {
                    var ratings = _dataContext.ratings
                                              .Where (x => x.webid == id)
                                              .OrderByDescending(x=>x.polarity);
                    var average = _dataContext.summary
                                              .FirstOrDefault(x => x.webid == id);

                    return new 
                    {
                         ratinglist    = ratings,
                         averagerating = average
                    };
               }
               
               return null;
          }

          private Object calculateratings (int webid) 
          {

               int[] polarity = new int[5] {1,2,3,4,5};

               var dbrecords = _dataContext.comments
                                           .Where (x => x.webid == webid);
               float Sum = 0;
               float TotalRatings = dbrecords.Count ();

               List<ratings> poloratings = new List<ratings> ();

               if (dbrecords.Count () > 3) 
               {

                    foreach (var pol in polarity) 
                    {
                         var polrating = dbrecords.Where (x => x.polarity == pol)
                                                  .Count ();

                         Sum += (polrating * pol);

                         float percentage = (polrating * 100) / TotalRatings;

                         Math.Round (percentage);

                         var rating = new ratings 
                         {
                              polarity = pol,
                              percentage = (int) percentage,
                              webid = webid
                         };
                         poloratings.Add (rating);
                    }

                    float avg = Sum / TotalRatings;
                          avg = (float) Math.Round (avg, 1);

                    return (new 
                    {
                         ratinglist = poloratings,
                         avgrating = new summary
                         {
                                   total   = (int) TotalRatings,
                                   average = avg,
                                   webid   = webid
                         }
                    });
               }
               return null;
          }

        public async Task<List<WebsiteReview>> Getallreviews(int id)
        {
               
               List<WebsiteReview> commentobj=new List<WebsiteReview>();

               var dbcomments=await _dataContext.comments
                                                .Where(x=>x.webid==id)
                                                .ToListAsync();

                   dbcomments.ForEach((comment)=>
                   {
                         var commentdto =setcomment(comment);

                             commentobj.Add(commentdto);
                   });                             
                  
               var orderdrecords=commentobj.OrderByDescending(x=>x.id)
                                           .ToList();

               return orderdrecords;
        }

        public async Task AddReview(reviewentity entity)
        {
                
                    var dbentity = _mapper.Map<comments>(entity);
                    
                              await _dataContext.comments
                                                .AddAsync(dbentity);

                              await _dataContext.SaveChangesAsync();

                    var ratings =_dataContext.ratings
                                             .FirstOrDefault(x=>x.webid==dbentity.webid);

                    var broadcastobj = setcomment(dbentity);  
                 
                    dynamic objs=calculateratings(entity.webid);
                        
                    if(objs!=null)
                    {
                         if(ratings!=null)
                         {
                                   dynamic ratingobj  = updateratings(objs,entity);
                                         
                                   List<ratings> newratings = ratingobj.ratinglist; 

                                        summary  avgrating  = ratingobj.avgrating;

                                         await _hubcontext.Clients.All
                                                          .SendAsync("sendratings",new Object[]{newratings,avgrating});
                         }
                         else
                         {
                                   dynamic newratingobj= getnewratings(objs); 
                                          
                                   List<ratings> newratings = newratingobj.newratings
                                                                          .ratinglist; 

                                        summary  avgrating  = newratingobj.newratings
                                                                          .avgrating;

                                         await _hubcontext.Clients.All
                                                          .SendAsync("newratings",new Object[]{newratings,avgrating});
                         }
                              await  _hubcontext.Clients
                                                .All.SendAsync("sendcomment",broadcastobj);  
                    }
                    else
                    {
                              await  _hubcontext.Clients
                                                .All.SendAsync("sendcomment",broadcastobj);
                    }

        }

        private WebsiteReview setcomment(comments commentdb)
        {
                
                var user =_dataContext.Users
                                      .Include(x=>x.profile)
                                      .FirstOrDefault(x=>x.Id==commentdb.userid);

                var userobj    = _mapper.Map<User,usertoreturn>(user);
                     
                var commentobj = _mapper.Map<usertoreturn,WebsiteReview>(userobj);    
                                 _mapper.Map<comments,WebsiteReview>(commentdb,commentobj);

                return commentobj;
        }

        private Object updateratings(dynamic objs,reviewentity entity)
        {
              
               var downobj=_dataContext.ratings
                                       .Where(x=>x.webid==entity.webid)
                                       .ToList();

               var downavg=_dataContext.summary
                                       .FirstOrDefault(x=>x.webid==entity.webid);
                                                 
                              List<ratings> ratings=objs.ratinglist;
                              
                              int index=0;
                              downobj.ForEach((items)=>
                              {
                                        items.percentage=ratings[index].percentage;
                                        items.polarity=ratings[index].polarity;
                                        items.webid=ratings[index].webid;
                                        index++;
                              });

                             _mapper.Map<summary,summary>(objs.avgrating,downavg);

                             _dataContext.ratings
                                         .UpdateRange(downobj);
                             _dataContext.summary
                                         .Update(downavg);
                             _dataContext.SaveChanges();
                             
               var orderdlist=downobj.OrderByDescending(x=>x.polarity)
                                     .ToList();
                   
               return new
              {
                 ratinglist=orderdlist,
                 avgrating =downavg
              };
         }


        private Object getnewratings(dynamic newratings)
        {
               
               _dataContext.ratings
                           .AddRange(newratings.ratinglist);
               _dataContext.summary
                           .Add(newratings.avgrating);
                                   
               _dataContext.SaveChanges();  

               return new
               {
                    newratings
               };
        }  
     }
}