using System;

namespace F19ITONK.ASPNETCore.MicroService.SecondFrontEnd.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}