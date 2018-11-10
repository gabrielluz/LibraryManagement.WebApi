using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace LibraryManager.Models.Dto
{
    public class ErrorDetails
    {
        public int Status { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}