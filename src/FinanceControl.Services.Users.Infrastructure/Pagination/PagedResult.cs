﻿using System.Collections.Generic;

namespace FinanceControl.Services.Users.Infrastructure.Pagination
{
    public class PagedResult<T>
    {
        /// <summary>
        /// The page number this page represents. 
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary> 
        /// The size of this page. 
        /// </summary> 
        public int PageSize { get; set; }

        /// <summary> 
        /// The total number of pages available. 
        /// </summary> 
        public int TotalNumberOfPages { get; set; }

        /// <summary> 
        /// The total number of records available. 
        /// </summary> 
        public int TotalNumberOfRecords { get; set; }

        /// <summary> 
        /// The records this page represents. 
        /// </summary> 
        public IEnumerable<T> Results { get; set; }
    }
}