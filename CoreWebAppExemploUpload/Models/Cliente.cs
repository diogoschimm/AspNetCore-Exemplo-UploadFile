using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAppExemploUpload.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }

        public IFormFile Foto { get; set; }
    }
}
