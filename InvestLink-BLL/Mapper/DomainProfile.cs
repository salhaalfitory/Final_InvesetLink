using AutoMapper;
using InvestLink_BLL.Models;
using InvestLink_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_BLL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Project, ProjectVM>();
            CreateMap<ProjectVM, Project>();
            //-----------------------

            CreateMap<FollowUpReport, FollowUpReportVM>();
            CreateMap<FollowUpReportVM, FollowUpReport>();
            //------------------------------------------------
            CreateMap<Nationality, NationalityVM>();
            CreateMap<NationalityVM, Nationality>();
            //---------------------------------
            CreateMap<Employee, EmployeeVM>();
            CreateMap<EmployeeVM, Employee>();
            ////---------------------------------
            CreateMap<Advertisement, AdvertisementVM>();
            CreateMap<AdvertisementVM, Advertisement>();
            ////---------------------------------
            CreateMap<Investor, InvestorVM>();
            CreateMap<InvestorVM, Investor>();
            ////---------------------------------
            CreateMap<License, LicenseVM>();
            CreateMap<LicenseVM, License>();
            ////---------------------------------
            CreateMap<ProjectInvestor, ProjectInvestorVM>();
            CreateMap<ProjectInvestorVM, ProjectInvestor>();
            ////---------------------------------
            CreateMap<ProjectFollowUp, ProjectFollowUpVM>();
            CreateMap<ProjectFollowUpVM, ProjectFollowUp>();
            ////---------------------------------
        }
    
    }
}
