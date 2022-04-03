using BigSchool_1811063011_ThanhLinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace BigSchool_1811063011_ThanhLinh.Controllers
{
    public class FollowingController : ApiController

    {
        [HttpPost]
        public IHttpActionResult Follow(Following follow)
        {
            var userID = User.Identity.GetUserId();
            if (userID == null)
                return BadRequest("Please login first!");
            if (userID == follow.FolloweeId)
                return BadRequest("Can not follow myself!");
            BigSchoolContext context = new BigSchoolContext();
            Following find = context.Followings.FirstOrDefault(p => p.FollwerId == userID
           && p.FolloweeId == follow.FolloweeId);
            if (find != null)
            {
                context.Followings.Remove(context.Followings.SingleOrDefault(p =>
                p.FollwerId == userID && p.FolloweeId == follow.FolloweeId));
                context.SaveChanges();
                return Ok("cancel");
            }
            follow.FollwerId = userID;
            context.Followings.Add(follow);
            context.SaveChanges();
            return Ok();
        }

    }
}