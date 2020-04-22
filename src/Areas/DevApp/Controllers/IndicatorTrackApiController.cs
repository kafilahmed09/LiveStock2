using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BES.Areas.DevApp.Models;
using BES.Data;
using BES.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BES.Areas.DevApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class IndicatorTrackApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IndicatorTrackApiController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/IndicatorTrackApi
        [HttpGet]
        public async Task<ActionResult<schoolIndicatorPictureRepository>> Get()
        //public async Task<ActionResult<pictureRepoitoryModel>> Get()
        {
            List<repositoryDetailList> repositoryDetailList = new List<repositoryDetailList>();
            repositoryDetailList image1 = new repositoryDetailList
            {
                current_date =  DateTime.Now.ToString(),
                picture_latitude = 11.11,
                picture_logitude = 22.22,
                picture_comment = "Picure Comments",
                picture_data = new sbyte[] { -1, 2, 3, 4, 5, 6, 7 },
                
            };

            repositoryDetailList.Add(image1);
            repositoryDetailList.Add(image1);
           


            pictureRepoitoryModel pictureRepoitory = new pictureRepoitoryModel
            {
                school_id = 1,
                indicatorID = 29,
                current_date = DateTime.Now.ToString(),
                pr_id=1,
                picture_count = 5,
               //  repositoryDetailList= repositoryDetailList

            };
            List<pictureRepoitoryModel> IndicatorPictureRepository = new List<pictureRepoitoryModel>();
            IndicatorPictureRepository.Add(pictureRepoitory);
            //schoolIndicatorPictureRepository.Add(pictureRepoitory);

            List<PictureRepository> schoolIndicatorPictureRepository = new List<PictureRepository>();
            PictureRepository SchoolIndicatorRepository = new PictureRepository
            {
                pictureRepoitoryModel = pictureRepoitory,
                repositoryDetailList = repositoryDetailList
            };
            schoolIndicatorPictureRepository.Add(SchoolIndicatorRepository);
            schoolIndicatorPictureRepository schoolIndicatorPictureRepositoryList = new schoolIndicatorPictureRepository()
            {
                schoolIndicatorPictureRepositoryList=schoolIndicatorPictureRepository
            };
            
            return schoolIndicatorPictureRepositoryList;
        }

        // POST: api/IndicatorTrackApi
        [HttpPost]
        public async Task<ActionResult<pictureRepoitoryModel>> Post(schoolIndicatorPictureRepository schoolIndicatorPictureRepositoryList)
        {
            bool status = true;
            string message = "Success";

            List<IndicatorDevApp> indicatorDevAppList = new List<IndicatorDevApp>();
            foreach (var RepoList in schoolIndicatorPictureRepositoryList.schoolIndicatorPictureRepositoryList)
            {
                
           
            var repo = RepoList.pictureRepoitoryModel;


                IndicatorTracking indicatorTracking = new IndicatorTracking
                {
                    IndicatorID = repo.indicatorID,
                    SchoolID = repo.school_id,
                    DateOfUpload = Convert.ToDateTime( repo.current_date),
                    IsUpload = true,
                    CreateDate = DateTime.Now,
                    TotalFilesUploaded = repo.picture_count,
                };
                //_context.Add(indicatorTracking);
                short i = 1;
                foreach(var repoDetail in RepoList.repositoryDetailList)
                {
                    IndicatorDevApp indicatorDevApp = new IndicatorDevApp
                    {
                        ImageID = i++,
                        SchoolID = repo.school_id,
                        IndicatorID = repo.indicatorID,
                        ImagePath = repoDetail.picture_data.ToString(), // SbyteToString(repoDetail.picture_data),
                        Longitude = repoDetail.picture_logitude,
                        Latitude = repoDetail.picture_latitude,
                        DateTime = Convert.ToDateTime( repoDetail.current_date),
                        SyncDate = DateTime.Now,
                        Remarks = repoDetail.picture_comment,

                    };

                 //   _context.Add(indicatorDevApp);
                }
            try
            {
                // await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IndicatorTrackingExists(repo.school_id, repo.indicatorID))
                {
                    status = false;
                    message = "Record Already Exist against this school and Indicator";
                    //return Conflict();
                }
                else
                {
                    throw;
                }



            }
 }
            



            return Ok(new { status, message });
            //  return CreatedAtAction("GetIndicatorTracking", new { id = indicatorTracking.SchoolID }, indicatorTracking);
        }

        public string SbyteToString (sbyte[] arr)
        {
            string buffer = "[";
            foreach(var a in arr)
            {
                buffer += a.ToString() + ",";
            }
            buffer += "]";
            return buffer;
        }


        private bool IndicatorTrackingExists(int? SchoolID, int? IndicatorID)
        {
            return _context.IncdicatorTracking.Any(e => e.SchoolID == SchoolID && e.IndicatorID == IndicatorID);
        }


    }
}
