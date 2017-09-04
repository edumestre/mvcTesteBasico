using System.Web.Mvc;

namespace CamadaUI.Controllers
{
    public class CursoController : BaseController
    {
        private CamadaAcessoDados.AcessoDados AcessoDados { get; set; }

        // GET: Curso
        public ActionResult Index()
        {
            try
            {
                this.AcessoDados = new CamadaAcessoDados.AcessoDados();
                var listaCursos = this.AcessoDados.ListarCursos();
                return View(listaCursos);
            }
            catch (System.Exception ex)
            {
                return TratarErro(ex);
            }
        }

        public ActionResult NovoCurso()
        {
            try
            {
                return View("Curso", new CamadaDTO.CursoDTO());
            }
            catch (System.Exception ex)
            {
                return TratarErro(ex);
            }
        }

        public ActionResult Curso(int idCurso)
        {
            try
            {
                this.AcessoDados = new CamadaAcessoDados.AcessoDados();
                var curso = this.AcessoDados.RetornarDetalheCurso(idCurso);
                return View(curso);
            }
            catch (System.Exception ex)
            {
                return TratarErro(ex);
            }
        }

        [HttpPost]
        public ActionResult Curso(CamadaDTO.CursoDTO curso)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.AcessoDados = new CamadaAcessoDados.AcessoDados();

                    if (curso.Id == 0)
                    {
                        return this.InserirCurso(curso);
                    }
                    else
                    {
                        return this.AlterarCurso(curso);
                    }
                }
                ViewBag.ErroPagina = "Erros de validação";
                return View(curso);
            }
            catch (System.Exception ex)
            {
                return TratarErro(ex);
            }
        }

        public ActionResult Delete(int idCurso)
        {
            try
            {
                if (idCurso > 0)
                {
                    this.AcessoDados = new CamadaAcessoDados.AcessoDados();
                    this.AcessoDados.DeletarCurso(idCurso);
                    Session["Sucesso"] = "Curso removido com sucesso";
                }
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                return TratarErro(ex, RedirectToAction("Index"));
            }
        }

        private ActionResult InserirCurso(CamadaDTO.CursoDTO curso)
        {
            var inseriu = this.AcessoDados.InserirCurso(curso);
            if (inseriu)
            {
                Session["Sucesso"] = "Curso inserido com sucesso";
                return RedirectToAction("Index");
            }
            ViewBag.ErroPagina = "Erros ao inserir o curso";
            return View(curso);
        }

        private ActionResult AlterarCurso(CamadaDTO.CursoDTO curso)
        {
            var alterou = this.AcessoDados.AlterarCurso(curso);
            if (alterou)
            {
                Session["Sucesso"] = "Curso alterado com sucesso";
                return RedirectToAction("Index");
            }
            ViewBag.ErroPagina = "Erros ao inserir o curso";
            return RedirectToRoute("Curso", new { idCurso = curso.Id });
        }
    }
}