namespace ResidentalManager.Web.ViewModels.Expences
{
    using System;
    using System.Collections.Generic;

    public class ExpencesListViewModel
    {
        public IEnumerable<AllExpencesViewModel> Expences { get; set; }

        public int PageNumber { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.ExpencesCount / this.ItemsPerPage);

        public int ExpencesCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
