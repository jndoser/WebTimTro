using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTimTro.Models
{
    public class NguoiDungVM
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public DateTime RegristeredAt { get; set; }
        public string Intro { get; set; }
        public string Profile { get; set; }
        public string Avatar { get; set; }
        [NotMapped]
        public IFormFile UploadFile { get; set; }
    }
}
