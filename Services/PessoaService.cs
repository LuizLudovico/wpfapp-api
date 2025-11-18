using System;
using System.Collections.Generic;
using System.Linq;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class PessoaService
    {
        private readonly IDataService<Pessoa> _dataService;

        public PessoaService()
        {
            _dataService = new JsonDataService<Pessoa>("pessoas.json");
        }

        public List<Pessoa> ObterTodas()
        {
            return _dataService.GetAll();
        }

        public Pessoa ObterPorId(Guid id)
        {
            return _dataService.GetById(id);
        }

        public List<Pessoa> BuscarPorNome(string nome)
        {
            return _dataService.GetAll()
                .Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase))
                .OrderBy(p => p.Nome)
                .ToList();
        }

        public List<Pessoa> BuscarPorCPF(string cpf)
        {
            return _dataService.GetAll()
                .Where(p => p.CPF.Contains(cpf))
                .ToList();
        }

        public void Adicionar(Pessoa pessoa)
        {
            _dataService.Add(pessoa);
            _dataService.SaveChanges();
        }

        public void Atualizar(Pessoa pessoa)
        {
            _dataService.Update(pessoa);
            _dataService.SaveChanges();
        }

        public void Excluir(Guid id)
        {
            _dataService.Delete(id);
            _dataService.SaveChanges();
        }

        public bool CPFJaExiste(string cpf, Guid? idExcluir = null)
        {
            return _dataService.GetAll()
                .Any(p => p.CPF == cpf && (!idExcluir.HasValue || p.Id != idExcluir.Value));
        }
    }
}

