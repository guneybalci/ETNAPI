using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Business.Enums
{
    public enum OrderStatus
    {
        [Description("Sipariş Alındı")]
        SiparisAlindi,
        [Description("Yola Çıktı")]
        YolaCikti,
        [Description("Dağıtım Merkezinde")]
        DagitimMerkezinde,
        [Description("Dağıtıma Çıktı")]
        DagitimaCikti,
        [Description("Teslim Edildi")]
        TeslimEdildi,
        [Description("Teslim Edilmedi")]
        TeslimEdilmedi
    }
}
