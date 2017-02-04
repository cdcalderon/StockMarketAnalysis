﻿using System;

namespace SMA.Core.Models
{
    public class HQuote
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Open { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public decimal? Close { get; set; }
        public decimal? Volume { get; set; }
    }
}
