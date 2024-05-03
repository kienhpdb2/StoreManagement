using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sellers.DTO
{
    public class ResponseDto<T> : ResponseDto
    {
        public T Data { get; private set; }
        public ResponseDto(string error) : base(error) { }
        public ResponseDto(T data) => Data = data;
        private ResponseDto() { }
    }

    public class ResponseDto
    {
        public string Error { get; private set; }
        public bool HasError => !string.IsNullOrEmpty(Error);
        public bool Success => !HasError;
        public ResponseDto() { }
        public ResponseDto(string error) => Error = error;
        public Guid TenantId { get; set; }
    }
}
