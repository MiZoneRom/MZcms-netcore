﻿using MZcms.DTO.QueryModel;
using MZcms.Entity;
using MZcms.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZcms.IServices
{
    public interface IPrivilegesService : IService
    {

        /// <summary>
        /// 添加一个平台管理员角色
        /// </summary>
        void AddPlatformRole(ManagersRoles model);

        /// <summary>
        /// 修改平台管理员角色
        /// </summary>
        void UpdatePlatformRole(ManagersRoles model);

        /// <summary>
        /// 删除一个平台角色
        /// </summary>
        /// <param name="id"></param>

        void DeletePlatformRole(long id);

        /// <summary>
        /// 获取一个平台角色的详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ManagersRoles GetPlatformRole(long id);

        /// <summary>
        /// 获取平台角色列表
        /// </summary>
        /// <returns></returns>
        IQueryable<ManagersRoles> GetPlatformRoles();

    }
}
