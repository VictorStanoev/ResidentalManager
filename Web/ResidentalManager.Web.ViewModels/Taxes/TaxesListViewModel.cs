namespace ResidentalManager.Web.ViewModels.Taxes
{
    using System;
    using System.Collections.Generic;

    public class TaxesListViewModel
    {
        public IEnumerable<TaxViewModel> Taxes { get; set; }

        public int PageNumber { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.TaxesCount / this.ItemsPerPage);

        public int TaxesCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
