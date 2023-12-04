﻿namespace ITCMS_HUIT.Client.Models
{
    public class ApiResponse<T>
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
