﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Resgrid.Framework;
using Resgrid.Model;
using Resgrid.Model.Repositories;
using Resgrid.Model.Repositories.Connection;
using Resgrid.Model.Repositories.Queries;
using Resgrid.Repositories.DataRepository.Configs;
using Resgrid.Repositories.DataRepository.Queries.Inventory;

namespace Resgrid.Repositories.DataRepository
{
	public class InventoryRepository : RepositoryBase<Inventory>, IInventoryRepository
	{
		private readonly IConnectionProvider _connectionProvider;
		private readonly SqlConfiguration _sqlConfiguration;
		private readonly IQueryFactory _queryFactory;
		private readonly IUnitOfWork _unitOfWork;

		public InventoryRepository(IConnectionProvider connectionProvider, SqlConfiguration sqlConfiguration, IUnitOfWork unitOfWork, IQueryFactory queryFactory)
			: base(connectionProvider, sqlConfiguration, unitOfWork, queryFactory)
		{
			_connectionProvider = connectionProvider;
			_sqlConfiguration = sqlConfiguration;
			_queryFactory = queryFactory;
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Inventory>> GetInventoryByTypeIdAsync(int typeId)
		{
			try
			{
				var selectFunction = new Func<DbConnection, Task<IEnumerable<Inventory>>>(async x =>
				{
					var dynamicParameters = new DynamicParameters();
					dynamicParameters.Add("TypeId", typeId);

					var query = _queryFactory.GetQuery<SelectInventoryByTypeIdQuery>();

					return await x.QueryFirstOrDefaultAsync<IEnumerable<Inventory>>(sql: query,
						param: dynamicParameters,
						transaction: _unitOfWork.Transaction);
				});

				DbConnection conn = null;
				if (_unitOfWork?.Connection == null)
				{
					using (conn = _connectionProvider.Create())
					{
						await conn.OpenAsync();

						return await selectFunction(conn);
					}
				}
				else
				{
					conn = _unitOfWork.CreateOrGetConnection();

					return await selectFunction(conn);
				}
			}
			catch (Exception ex)
			{
				Logging.LogException(ex);

				throw;
			}
		}

		public async Task<IEnumerable<Inventory>> GetAllInventoriesByDepartmentIdAsync(int departmentId)
		{
			try
			{
				var selectFunction = new Func<DbConnection, Task<IEnumerable<Inventory>>>(async x =>
				{
					var dynamicParameters = new DynamicParameters();
					dynamicParameters.Add("DepartmentId", departmentId);

					var query = _queryFactory.GetQuery<SelectInventoryByDIdQuery>();

					return await x.QueryAsync<Inventory, InventoryType, Inventory>(sql: query,
						param: dynamicParameters,
						transaction: _unitOfWork.Transaction,
						map: (i, it) => { i.Type = it; return i; },
						splitOn: "InventoryTypeId");
				});

				DbConnection conn = null;
				if (_unitOfWork?.Connection == null)
				{
					using (conn = _connectionProvider.Create())
					{
						await conn.OpenAsync();

						return await selectFunction(conn);
					}
				}
				else
				{
					conn = _unitOfWork.CreateOrGetConnection();

					return await selectFunction(conn);
				}
			}
			catch (Exception ex)
			{
				Logging.LogException(ex);

				throw;
			}
		}

		public async Task<Inventory> GetInventoryByIdAsync(int inventoryId)
		{
			try
			{
				var selectFunction = new Func<DbConnection, Task<Inventory>>(async x =>
				{
					var dynamicParameters = new DynamicParameters();
					dynamicParameters.Add("InventoryId", inventoryId);

					var query = _queryFactory.GetQuery<SelectInventoryByInventoryIdQuery>();

					return (await x.QueryAsync<Inventory, InventoryType, Inventory>(sql: query,
						param: dynamicParameters,
						transaction: _unitOfWork.Transaction,
						map: (i, it) => { i.Type = it; return i; },
						splitOn: "InventoryTypeId")).FirstOrDefault();
				});

				DbConnection conn = null;
				if (_unitOfWork?.Connection == null)
				{
					using (conn = _connectionProvider.Create())
					{
						await conn.OpenAsync();

						return await selectFunction(conn);
					}
				}
				else
				{
					conn = _unitOfWork.CreateOrGetConnection();

					return await selectFunction(conn);
				}
			}
			catch (Exception ex)
			{
				Logging.LogException(ex);

				throw;
			}
		}
	}
}
