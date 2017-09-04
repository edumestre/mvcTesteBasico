using System;
using System.Web.Mvc;

namespace CamadaUI.Controllers
{
    public class AlunoController : BaseController
    {
        private CamadaAcessoDados.AcessoDados AcessoDados { get; set; }

        // GET: Aluno
        public ActionResult Index(int idCurso)
        {
            try
            {
                ViewBag.IdCurso = idCurso;
                this.AcessoDados = new CamadaAcessoDados.AcessoDados();
                var listaAlunos = this.AcessoDados.ListarAlunosCurso(idCurso);
                return View(listaAlunos);
            }
            catch (System.Exception ex)
            {
                return TratarErro(ex);
            }
        }

        public ActionResult NovoAluno(int idCurso)
        {
            try
            {
                ViewBag.IdCurso = idCurso;
                return View("Aluno", new CamadaDTO.AlunoDTO() { IdCurso = idCurso });
            }
            catch (System.Exception ex)
            {
                return TratarErro(ex);
            }
        }

        public ActionResult Aluno(int idAluno)
        {
            try
            {
                this.AcessoDados = new CamadaAcessoDados.AcessoDados();
                var aluno = this.AcessoDados.RetornarDetalheAluno(idAluno);
                return View(aluno);
            }
            catch (System.Exception ex)
            {
                return TratarErro(ex);
            }
        }

        [HttpPost]
        public JsonResult Aluno(CamadaDTO.AlunoDTO aluno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.AcessoDados = new CamadaAcessoDados.AcessoDados();

                    if (aluno.Id == 0)
                    {
                        return this.InserirAluno(aluno);
                    }
                    else
                    {
                        return this.AlterarAluno(aluno);
                    }
                }

                return new JsonResult
                {
                    Data = new { erro = "Erros de validação - verifique os campos digitados" }, // aqui daria para serializar o ModelState.Errors e mandar
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (System.Exception ex)
            {
                return TratarErroJson(ex);
            }
        }

        public ActionResult Delete(int idAluno)
        {
            try
            {
                if (idAluno > 0)
                {
                    this.AcessoDados = new CamadaAcessoDados.AcessoDados();
                    var aluno = this.AcessoDados.RetornarDetalheAluno(idAluno);
                    if (aluno != null)
                    {
                        this.AcessoDados.DeletarAluno(idAluno);
                        Session["Sucesso"] = "Aluno removido com sucesso";
                        return RedirectToAction("Index", new { idCurso = aluno.IdCurso });
                    }
                }
                return RedirectToAction("Index", "Curso", null);
            }
            catch (System.Exception ex)
            {
                return TratarErro(ex, RedirectToAction("Index", "Curso", null));
            }
        }

        private JsonResult InserirAluno(CamadaDTO.AlunoDTO aluno)
        {
            var inseriu = this.AcessoDados.InserirAluno(aluno);
            if (inseriu)
            {
                return new JsonResult
                {
                    Data = new { sucesso = "Aluno inserido com sucesso" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            return new JsonResult
            {
                Data = new { erro = "Erros ao inserir o aluno" },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        private JsonResult AlterarAluno(CamadaDTO.AlunoDTO aluno)
        {
            var alterou = this.AcessoDados.AlterarAluno(aluno);
            if (alterou)
            {
                return new JsonResult
                {
                    Data = new { sucesso = "Aluno alterado com sucesso" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            return new JsonResult
            {
                Data = new { erro = "Erros ao alterar o aluno" },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}