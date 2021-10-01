using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Context;
using MVC.Aplicacao;
using MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MVC.Controllers
{
    public class ClientePFController : Controller
    {

        private readonly BancoContext _context;

        public ClientePFController(BancoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ListaClientes()
        {
            /*var objAplicacao = new ClientePFAplicattion(_context);
            List<Cliente> lstCli = objAplicacao.GetAllClients();
            return View(lstCli);*/

           var bancoContext = _context.Clientes.Include(c => c.CodTipoCliNavigation);

            return View(await bancoContext.ToListAsync());
        }


        public async Task<IActionResult> GetClientePF(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.CodCli == id);
              //  .Include(c => c.CodTipoCliNavigation)
                //.FirstOrDefaultAsync(m => m.CodCli == CodCli);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: ClientePF/Create
        public ActionResult Create()
        {
            ViewData["CodTipoCli"] = new SelectList(_context.TipoClis.Where(c => c.CodTipoCli == 1), "CodTipoCli", "NomeTipoCli");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodCli,CodTipoCli,Nome,DataNascFund,RendaLucro,Email,Sexo,Endereco,Documento")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListaClientes));
            }
            ViewData["CodTipoCli"] = new SelectList(_context.TipoClis.Where(c => c.CodTipoCli == 1), "CodTipoCli", "NomeTipoCli", cliente.CodTipoCli);
            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cli = await _context.Clientes.FindAsync(id);
            if (cli == null)
            {
                return NotFound();
            }
            ViewData["CodTipoCli"] = new SelectList(_context.TipoClis.Where(c => c.CodTipoCli == 1), "CodTipoCli", "NomeTipoCli", cli.CodTipoCli);
              return View(cli);
        }

        // POST: Clientes/Edit/5
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodCli,CodTipoCli,Nome,DataNascFund,RendaLucro,Email,Sexo,Endereco,Documento")] Cliente cliente)
        {
            if (id != cliente.CodCli)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.CodCli))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ListaClientes));
            }
            ViewData["CodTipoCli"] = new SelectList(_context.TipoClis.Where(c=>c.CodTipoCli == 1), "CodTipoCli", "NomeTipoCli", cliente.CodTipoCli);
             return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cliente = await _context.Clientes
                .Include(c => c.CodTipoCliNavigation)
                .FirstOrDefaultAsync(m => m.CodCli == id);
            if (cliente == null)
            {
                return NotFound();
            }
            else
            {
                var conta = await _context.Conta.Include(c => c.CodCliNavigation)
                                              .FirstOrDefaultAsync(i => i.CodCli == id);

                if(conta != null )
                {

                    return RedirectToAction("StatusDelete");
                }
            }

            return View(cliente);
        }

        public IActionResult StatusDelete()
        {
            return View();
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int CodCli)
        {
            var cliente = await _context.Clientes.FindAsync(CodCli);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListaClientes));
        }

        private bool ClienteExists(int codCli)
        {
            return _context.Clientes.Any(e => e.CodCli == codCli);
        }


    }
}
