using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Twajd_Back_End.Core.Models;
using Twajd_Back_End.Core.Repositories;
using Twajd_Back_End.Core.Services;
using Microsoft.AspNetCore.JsonPatch;

namespace Twajd_Back_End.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyService _companyService;
        public CompaniesController(IUnitOfWork unitOfWork, ICompanyService companyService)
        {
            _unitOfWork = unitOfWork;
            _companyService = companyService;
        }

        //GET: api/Companies
       [HttpGet]
        public async Task<IEnumerable<Company>> GetCompanies()
        {
            //var companies = _unitOfWork.CompanyRepository.GetAll();
            var companies = await _companyService.Get();
            return companies;
        }

        [HttpGet("/aatest")]
        public async Task<IEnumerable<Company>> GetCompanies2()
        {
            //var companies = _unitOfWork.CompanyRepository.GetAll();
            var companies = await _unitOfWork.CompanyRepository.Get();
            return companies;
            //return await _context.Companies.ToListAsync();
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(Guid id)
        {
            var company = await _unitOfWork.CompanyRepository.GetById(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        //// PUT: api/Companies/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<ActionResult<Company>> PutCompany(Company company)
        {
            if (company.Id == null)
            {
                return NotFound();
            }

            _unitOfWork.CompanyRepository.Update(company);
            _unitOfWork.Commit();
            return await _unitOfWork.CompanyRepository.GetById(company.Id);
        }


        //// PATCH: api/Companies/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPatch]
        public async Task<ActionResult<Company>> PatchCompany(Guid id, JsonPatchDocument<Company> patchCompany)
        {
            if (patchCompany == null || id == null)
            {
                return BadRequest(ModelState);
            }

            Company companyFromDb = await _unitOfWork.CompanyRepository.GetById(id);
            if (companyFromDb == null)
            {
                return NotFound();
            }

            patchCompany.ApplyTo(companyFromDb, ModelState);

            bool isValid = TryValidateModel(companyFromDb);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.CompanyRepository.Update(companyFromDb);
            _unitOfWork.Commit();
            return companyFromDb;
        }

        //    if (id != company.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(company).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CompanyExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Companies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public /*async Task<ActionResult<Company>>*/ void PostCompany(Company company)
        {
            _unitOfWork.CompanyRepository.Insert(company);
            _unitOfWork.Commit();
            //_unitOfWork.CompanyRepository.Insert(company);
            //_unitOfWork.Commit();
            //return _unitOfWork.CompanyRepository.GetById(company.Id);
            //_context.Companies.Add(company);
            //await _context.SaveChangesAsync();

            // return CreatedAtAction("GetCompany", new { id = company.Id }, company);
        }

        //// DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompany(Guid id)
        {
            Company company = await _unitOfWork.CompanyRepository.GetById(id);
            if (company == null)
            {
                return NotFound();
            }

            _unitOfWork.CompanyRepository.Delete(id);
            _unitOfWork.Commit();
            return Ok();
            //    var company = await _context.Companies.FindAsync(id);
            //    if (company == null)
            //    {
            //        return NotFound();
            //    }

            //    _context.Companies.Remove(company);
            //    await _context.SaveChangesAsync();

            //    return company;
            //}

            //private bool CompanyExists(Guid id)
            //{
            //    return _context.Companies.Any(e => e.Id == id);
            //}
        }
    }
}
