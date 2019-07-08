using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Controllers
{

    /* 
    
     */
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly IDatingRepository _repo;
        private Cloudinary _cloudinary;
        private readonly IMapper _mapper;

        public AdminController(DataContext context, UserManager<User> userManager, IDatingRepository repo,
        IOptions<CloudinarySettings> cloudinaryConfig, IMapper mapper)
        {
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;
            _userManager = userManager;
            _context = context;
            _repo = repo;

            Account acc = new Account(
             _cloudinaryConfig.Value.CloudName,
             _cloudinaryConfig.Value.ApiKey,
             _cloudinaryConfig.Value.ApiSecret

         );

            _cloudinary = new Cloudinary(acc);

        }


        /* 
        
         */
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("usersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var userList = await (from user in _context.Users
                                  orderby user.Id descending     //linq - kysely
                                  select new
                                  {
                                      Id = user.Id,
                                      UserName = user.UserName,
                                      Roles = (from userRole in user.UserRoles
                                               join role in _context.Roles
                                               on userRole.RoleId
                                               equals role.Id
                                               select role.Name).ToList()
                                  }).ToListAsync();

            return Ok(userList);
        }


        /* 
        
         */

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("editRoles/{userName}")]
        public async Task<IActionResult> EditRoles(string userName, RoleEditDto roleEditDto)
        {
            var user = await _userManager.FindByNameAsync(userName);

            var userRoles = await _userManager.GetRolesAsync(user);

            var selectedRoles = roleEditDto.RoleNames;
            // selected = selectedRoles != null ? selectedRoles : new string[] {};  sama kuin allaoleva
            selectedRoles = selectedRoles ?? new string[] { }; //null coalescing operaattori

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to add to roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
                return BadRequest("failed to remove the roles");

            return Ok(await _userManager.GetRolesAsync(user));

        }

        /* 
        
         */

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("photosForModeration")]
        public  IActionResult GetPhotosForModeration()
        {

            var photosToMap = _context.Photos.Where(p => p.IsApproved == false);
            PhotosForDetailedDto[] unApprovedPhotos = _mapper.Map<PhotosForDetailedDto[]>(photosToMap);


            /* await (from photo in _context.Photos.Where(p => p.IsApproved == false)
                                          orderby photo.Id descending
                                          select new
                                          {
                                              Id = photo.Id,
                                              PhotoUrl = photo.Url,
                                              IsApproved = photo.IsApproved
                                          }) */

            return Ok(unApprovedPhotos);
        }

        /* 

        */
        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpPost("approvePhoto/{id}")]
        public async Task<IActionResult> ApprovePhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);
            if (photoFromRepo.IsApproved == true)
                return BadRequest("photo is already approved");

            var userFromRepo = await _repo.GetUser(photoFromRepo.UserId);
             if (!userFromRepo.Photos.Any(u => u.IsMain))
                 photoFromRepo.IsMain = true;

            photoFromRepo.IsApproved = true;

            if (await _repo.SaveAll())
                return NoContent();
            return BadRequest("Could not approve photo!");
        }


        /* 
        
        */
        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpDelete("rejectPhoto/{id}")]
        public async Task<IActionResult> RejectPhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);
            if (photoFromRepo.IsApproved)
                return BadRequest("This photo is already approved");

            if (photoFromRepo.PublicId != null)
            {
                var deleteParams = new DeletionParams(photoFromRepo.PublicId);
                var result = _cloudinary.Destroy(deleteParams);
                if (result.Result == "ok")
                {
                    _repo.Delete(photoFromRepo);
                }
            }

            if (photoFromRepo.PublicId == null)
            {
                _repo.Delete(photoFromRepo);
            }

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to reject the photo");
        }
    }
}
