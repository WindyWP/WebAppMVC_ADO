using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebAppMVC_ADO.Models
{
    public class Kendaraan_Model
    {
        [Key]
        public int KendaraanID { get; set; }

        [Required]
        [DisplayName("Nama Kendaraan")]
        public string KendaraanName { get; set; }

        [Required]
        public int Harga { get; set; }

        [Required]
        public int Jumlah { get; set; }
        public string Keterangan { get; set; }
    }
}