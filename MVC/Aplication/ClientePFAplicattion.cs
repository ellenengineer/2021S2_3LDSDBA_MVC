using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Context;
using MVC.Models;

namespace MVC.Aplicacao
{
    public class ClientePFAplicattion
    {
        private BancoContext _contexto;

        public ClientePFAplicattion(BancoContext contexto)
        {
            _contexto = contexto;
        }

        public string InserirCliente(Cliente cli)
        {
            try
            {
                if (cli != null)
                {
                    var clienteExiste = GetCliByID(cli.CodCli);

                    if (clienteExiste == null)
                    {
                        cli.CodTipoCli = 1;

                        _contexto.Add(cli);
                        _contexto.SaveChanges();

                        return "Cliente cadastrado com sucesso!";
                    }
                    else
                    {
                        return "Cliente já cadastrado na base de dados.";
                    }
                }
                else
                {
                    return "Cliente inválido!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public string AtualizarCliente(Cliente cli)
        {
            try
            {
                if (cli != null)
                {
                    _contexto.Update(cli);
                    _contexto.SaveChanges();

                    return "Cliente alterado com sucesso!";
                }
                else
                {
                    return "Cliente inválido!";
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

        public Cliente GetCliByID(int codCli)
        {
            Cliente primeiroCliente = new Cliente();

            try
            {
                if (codCli == 0)
                {
                    return null;
                }

                var cli = _contexto.Clientes.Where(x => x.CodCli == codCli && x.CodTipoCli == 1).ToList();
                primeiroCliente = cli.FirstOrDefault();

                if (primeiroCliente != null)
                {
                    return primeiroCliente;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Cliente> GetAllClients()
        {
            List<Cliente> listaDClientes = new List<Cliente>();
            try
            {

                listaDClientes = _contexto.Clientes.Where(p=>p.CodTipoCli == 1).ToList(); //_contexto.Clientes.Select(x => x).ToList();


                foreach (var item in listaDClientes)
                {
                    item.DataNascFund = Convert.ToDateTime(item.DataNascFund.ToString("d")).Date;
                }
                if (listaDClientes != null)
                {
                    return listaDClientes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string DeleteClientByCod(int codCli)
        {
            try
            {
                if (codCli == 0)
                {
                    return "Cliente inválido! Por favor tente novamente.";
                }
                else
                {
                    var cli = GetCliByID(codCli);

                    if (cli != null)
                    {
                        _contexto.Clientes.Remove(cli);
                        _contexto.SaveChanges();

                        return "Cliente " + cli.Nome + " deletado com sucesso!";
                    }
                    else
                    {
                        return "Cliente não cadastrado!";
                    }
                }
            }
            catch (Exception)
            {
                return "Não foi possível se comunicar com a base de dados!";
            }
        }

    }
}

