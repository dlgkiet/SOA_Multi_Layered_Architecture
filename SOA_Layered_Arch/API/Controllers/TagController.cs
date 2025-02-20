using Microsoft.AspNetCore.Mvc;
using SOA_Layered_Arch.CoreLayer.Entities;
using SOA_Layered_Arch.ServiceLayer;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SOA_Layered_Arch.APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly TagService _tagService;

        public TagController(TagService tagService)
        {
            _tagService = tagService ?? throw new ArgumentNullException(nameof(tagService));
        }

        // ✅ Lấy tất cả tag
        [HttpGet]
        public async Task<IActionResult> GetAllTags(CancellationToken cancellationToken)
        {
            var tags = await _tagService.GetAllTagsAsync(cancellationToken);
            return Ok(tags);
        }

        // ✅ Lấy tag theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTag(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest("ID must be greater than zero.");

            var tag = await _tagService.GetTagByIdAsync(id, cancellationToken);
            if (tag == null)
                return NotFound();

            return Ok(tag);
        }

        // ✅ Thêm một tag mới
        [HttpPost]
        public async Task<IActionResult> AddTag([FromBody] Tag tag, CancellationToken cancellationToken)
        {
            if (tag == null)
                return BadRequest("Tag data is required.");

            // Đảm bảo rằng TagId không có giá trị (để SQL Server tự sinh)
            tag.TagId = 0;
            var createdTag = await _tagService.AddTagAsync(tag, cancellationToken);
            return CreatedAtAction(nameof(GetTag), new { id = createdTag.TagId }, createdTag);
        }

        // ✅ Cập nhật thông tin tag
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(int id, [FromBody] Tag tag, CancellationToken cancellationToken)
        {
            if (id <= 0 || tag == null || id != tag.TagId)
                return BadRequest("Invalid tag data.");

            var updatedTag = await _tagService.UpdateTagAsync(tag, cancellationToken);
            if (updatedTag == null)
                return NotFound();

            return Ok(updatedTag);
        }

        // ✅ Xóa một tag
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest("ID must be greater than zero.");

            var deleted = await _tagService.DeleteTagAsync(id, cancellationToken);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
