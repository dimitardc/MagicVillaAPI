using AutoMapper;
using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MagicVilla_Web.Controllers
{
    public class VillaController : Controller
    {

        private readonly IVillaService _service;
        private readonly IMapper _mapper;

        public VillaController(IVillaService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDTO> list = new List<VillaDTO>();

            var response = await _service.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        [Authorize(Roles ="admin")]
        public IActionResult CreateVilla()
        {

            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]  //always have this for post methods
        public async Task<IActionResult> CreateVilla(VillaCreateDTO createDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.CreateAsync<APIResponse>(createDTO, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa created successfully";
                    return RedirectToAction("IndexVilla");
                }
            }
            TempData["error"] = "Error";

            //return back to the same view with the model to show validation errors
            return View(createDTO);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVilla(int villaId)
        {
            var response = await _service.GetAsync<APIResponse>(villaId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {

                VillaDTO villaDTO = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                VillaUpdateDTO villaUpdateDTO = _mapper.Map<VillaUpdateDTO>(villaDTO);
                return View(villaUpdateDTO);
            }


            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDTO updateDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.UpdateAsync<APIResponse>(updateDTO, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa updated successfully";

                    return RedirectToAction("IndexVilla");
                }
            }
            TempData["error"] = "Error";

            return View(updateDTO);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVilla(int villaId)
        {
            var response = await _service.GetAsync<APIResponse>(villaId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                VillaDTO villaDTO = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(villaDTO);
            }


            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaDTO DTO)
        {

            var response = await _service.DeleteAsync<APIResponse>(DTO.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa deleted successfully";

                return RedirectToAction("IndexVilla");
            }
            TempData["error"] = "Error";

            return View(DTO);
        }
    }
}
