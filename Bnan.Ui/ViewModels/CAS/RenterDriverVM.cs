﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bnan.Ui.ViewModels.CAS
{
    public class RenterDriverVM 
    {
       
        [Required(ErrorMessage = "requiredFiled")]
        public string CrCasRenterPrivateDriverInformationId { get; set; } = null!;
        public string CrCasRenterPrivateDriverInformationLessor { get; set; } = null!;
        public string? CrCasRenterPrivateDriverInformationIdtrype { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasRenterPrivateDriverInformationArName { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasRenterPrivateDriverInformationEnName { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public DateTime? CrCasRenterPrivateDriverInformationBirthDate { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public DateTime? CrCasRenterPrivateDriverInformationIssueIdDate { get; set; }

        [Required(ErrorMessage = "requiredFiled")]
        public DateTime? CrCasRenterPrivateDriverInformationExpiryIdDate { get; set; }
        public string? CrCasRenterPrivateDriverInformationLicenseNo { get; set; }

        public string? CrCasRenterPrivateDriverInformationLicenseType { get; set; }

        [Required(ErrorMessage = "requiredFiled")]
        public DateTime? CrCasRenterPrivateDriverInformationLicenseDate { get; set; }

        [Required(ErrorMessage = "requiredFiled")]
        public DateTime? CrCasRenterPrivateDriverInformationLicenseExpiry { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasRenterPrivateDriverInformationNationality { get; set; }
        [Required(ErrorMessage = "requiredFiled")]
        public string? CrCasRenterPrivateDriverInformationGender { get; set; }
        public string? CrCasRenterPrivateDriverInformationKeyMobile { get; set; }
        public string? CrCasRenterPrivateDriverInformationMobile { get; set; }
        public string? CrCasRenterPrivateDriverInformationEmail { get; set; }
        public DateTime? CrCasRenterPrivateDriverInformationLastContract { get; set; }
        public int? CrCasRenterPrivateDriverInformationContractCount { get; set; }
        public int? CrCasRenterPrivateDriverInformationDaysCount { get; set; }
        public int? CrCasRenterPrivateDriverInformationTraveledDistance { get; set; }
        public decimal? CrCasRenterPrivateDriverInformationEvaluationTotal { get; set; }
        public decimal? CrCasRenterPrivateDriverInformationEvaluationValue { get; set; }
        public string? CrCasRenterPrivateDriverInformationSignature { get; set; }
        public string? CrCasRenterPrivateDriverInformationIdImage { get; set; }
        public string? CrCasRenterPrivateDriverInformationLicenseImage { get; set; }
        public string? CrCasRenterPrivateDriverInformationStatus { get; set; }
        public string? CrCasRenterPrivateDriverInformationReasons { get; set; }
    }
}
