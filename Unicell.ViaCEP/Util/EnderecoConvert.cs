﻿using System.Collections.Generic;
using Unicell.ViaCep.Modelos;

namespace Unicell.ViaCep.Util
{
    public sealed class EnderecoConvert : IEnderecoConvert
    {
        public Endereco DeJsonParaEndereco(string json)
            => Newtonsoft.Json.JsonConvert.DeserializeObject<Endereco>(json);

        public IEnumerable<Endereco> DeJsonParaListaDeEnderecos(string json)
            => Newtonsoft.Json.JsonConvert.DeserializeObject<List<Endereco>>(json);
    }
}