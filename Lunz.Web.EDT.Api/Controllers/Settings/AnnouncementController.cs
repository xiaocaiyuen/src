using Lunz.Data.EDT;
using Lunz.Services;
using Lunz.Services.EDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Lunz.Web.EDT.Api.Controllers
{
    public class AnnouncementController:BaseApiController
    {
        private readonly IAnnouncementService _announcementService;

        /// <summary>
        /// 初始化 AnnouncementController 类的新实例
        /// </summary>
        /// <param name="seatServcie"></param>
        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        /// <summary>
        /// 获取 通知公告 列表
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <returns>成功返回 '通知公告' 列表</returns>
        [ActionName("List")]
        public WebApiPagingResult<IQueryable<Basic_Announcement>> GetList([FromUri]PagingOptions paging)
        {
            return _announcementService.List(paging);
        }

        /// <summary>
        /// 新建 通知公告
        /// </summary>
        /// <param name="entity">要新建的通知公告参数</param>
        /// <returns>成功返回新建的通知公告参数</returns>
        [ActionName("Add")]
        public WebApiResult<Basic_Announcement> PostAdd([FromBody]Basic_Announcement entity)
        {
            ILogService logService = new LogService();
            logService.Info("新增一条通知公告");
            return _announcementService.Add(entity);
        }

        /// <summary>
        /// 更新 通知公告参数
        /// </summary>
        /// <param name="entity">要更新的通知公告参数</param>
        /// <returns>成功返回更新后的通知公告参数</returns>
        [ActionName("Update")]
        public WebApiResult<Basic_Announcement> PostUpdate([FromBody]Basic_Announcement entity)
        {
            ILogService logService = new LogService();
            logService.Info("更新一条通知公告");
            return _announcementService.Update(entity);
        }
        /// <summary>
        /// 删除 通知公告参数
        /// </summary>
        /// <param name="id">要删除的通知公告参数主键列表</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        [ActionName("Delete")]
        public WebApiResult PostDelete([FromBody]Guid[] id)
        {
            ILogService logService = new LogService();
            logService.Info("删除一条通知公告");
            return _announcementService.Delete(id);
        }
    }
}