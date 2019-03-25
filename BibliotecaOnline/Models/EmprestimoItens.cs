﻿using BibliotecaOnline.Models.Enum;
using System;

namespace BibliotecaOnline.Models
{
    public class EmprestimoItens
    {
        public EmprestimoItens(int colaboradorId, int usuarioId, DateTime dataEmprestimo, int emprestimoId)
        {
            ColaboradorId = colaboradorId;
            UsuarioId = usuarioId;
            DataEmprestimo = dataEmprestimo;
            Status = LivroExemplarStatusEnum.Empresatado;
            EmprestimoId = emprestimoId;
        }

        public EmprestimoItens()
        {
        }

        public int Id { get; set; }
        public int ColaboradorId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataRenovacao { get; set; }
        public int ColaboradorIdRenovacao { get; set; }
        public DateTime DataDevolucao { get; set; }
        public int ColaboradorIdDevolucao { get; set; }
        public LivroExemplarStatusEnum Status { get; set; }

        public int EmprestimoId { get; set; }
        public Emprestimo Emprestimos { get; set; }
        public int ExemplarId { get; set; }
        public LivroExemplar Exemplares { get; set; }

        public void Renovar()
        {
            Status = LivroExemplarStatusEnum.Reservado;
            DataRenovacao = DateTime.Now;
            ColaboradorIdRenovacao = 0;
        }
        public void Devolucao()
        {
            Status = LivroExemplarStatusEnum.Devolvido;
            DataDevolucao = DateTime.Now;
        }
    }
}