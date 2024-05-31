using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Articles;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers.Articles
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize(Roles = "Administrator")]
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }
        [SwaggerOperation("Returns a list of articles.")]
        [HttpGet, AllowAnonymous]
        public async Task<ArticleResult.Index> GetIndex([FromQuery] ArticleRequest.Index request)
        {
            return await articleService.GetIndexAsync(request);
        }

        [SwaggerOperation("Creates an article.")]
        [HttpPost]
        public async Task<IActionResult> Create(ArticleDto.Mutate model)
        {
            var creationId = await articleService.CreateAsync(model);
            return CreatedAtAction(nameof(Create), creationId);
        }

        [SwaggerOperation("Returns a specific article.")]
        [HttpGet("{Id}"), AllowAnonymous]
        public async Task<ArticleDto.Detail> GetDetails(long Id) {
            return await articleService.GetDetailAsync(Id);
        }

        [SwaggerOperation("Edits a specific article.")]
        [HttpPut("{Id}"), AllowAnonymous]
        public async Task Edit(long Id, ArticleDto.Detail model)
        {
            await articleService.EditAsync(Id, model);
        }

        [SwaggerOperation("Deletes an existing article.")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await articleService.DeleteAsync(id);
            return NoContent();
        }

    }
}
