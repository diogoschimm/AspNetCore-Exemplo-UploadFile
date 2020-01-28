# AspNetCore-Exemplo-UploadFile
Exemplo de Upload no AspNetCore com Model

## Criar a model com o atributo IFormFile

```c#
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
```

## Criar a View

Colocar o enctype do form para "multipart/form-data", colocar o controller e action


```cshtml
@model CoreWebAppExemploUpload.Models.Cliente

@{
    ViewData["Title"] = "Index";
}

<h1>Index</h1>

<form enctype="multipart/form-data" class="form" asp-controller="Home" asp-action="index">

    <label>ID Cliente</label>
    <input asp-for="ClienteId" />

    <label>Nome do Cliente</label>
    <input asp-for="Nome" />

    <label>Foto do Cliente</label>
    <input asp-for="Foto" type="file" />

    <br />
    <br />

    <input type="submit" />
</form>
```

## Criar o Controller para receber os dados da View 

Observação: O sistema converte a Foto para Base64 e envia para a View ExibeDados atraves de ViewBag.
Se for armazenar armazenar no banco de dados em um varbinary(max) utilizar o metodo ConverterToByteArray.


```c#
    public class HomeController : Controller
    { 
        public IActionResult Index()
        {
            return View(new Cliente { ClienteId = 1, Nome = "Diogo Rodrigo" });
        }

        [HttpPost]
        public IActionResult Index(Cliente cliente)
        { 
            ViewBag.Imagem = ConverterToBase64(cliente.Foto);

            return View("ExibeDados", cliente);
        }


        private string ConverterToBase64(Microsoft.AspNetCore.Http.IFormFile file)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                file.CopyTo(ms);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        
        private byte[] ConverterToByteArray(Microsoft.AspNetCore.Http.IFormFile file)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
```

## Criar a View para exibir os dados do Upload

O sistema utiliza a ViewBag Imagem que contém a imagem codificada em Base64 e exibe em uma img do html

```cshtml
@model CoreWebAppExemploUpload.Models.Cliente

@{
    ViewData["Title"] = "ExibeDados";
}

@{
    var imgSrc = String.Format("data:image/gif;base64,{0}", ViewBag.Imagem);
}

  <h1>ExibeDados</h1>

  ID: @Model.ClienteId
  <br />
  
  Nome: @Model.Nome
  <br />

  <br />
  Image:
  <img src="@imgSrc" />
```

