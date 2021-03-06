﻿using BibliotecaOnline.Models.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaOnline.Models.ViewModel
{
    public class EmprestimoItensViewModel
    {
        public int EmprestimoId { get; set; }
        public string CodigoDeBarras { get; set; }
        public string Titulo { get; set; }
        public string Edicao { get; set; }
        public string Idioma { get; set; }
        public string Editora { get; set; }
        public string Autor { get; set; }
        public string Campus { get; set; }
        public string Usuario { get; set; }
        public string Matricula { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime DataEmprestimo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataPrevisao { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataDevolucao { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Renovacao { get; set; }

        public int? diasRestante { get; set; }

        public LivroExemplarStatusEnum Status { get; set; }
    }
}