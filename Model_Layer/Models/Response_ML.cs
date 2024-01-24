using System;
using System.Collections.Generic;
using System.Text;

namespace Model_Layer.Models
{
    public class Response_ML<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
