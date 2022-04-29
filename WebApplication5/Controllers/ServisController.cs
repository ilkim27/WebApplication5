using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication5.Models;
using WebApplication5.ViewModel;

namespace WebApplication5.Controllers
{
    public class ServisController : ApiController
    {
        Database1Entities db = new Database1Entities();
        sonucModel sonuc = new sonucModel();

        #region Dosya
        

        [HttpGet]
        [Route("api/dersliste")]
        public List<DosyaModel> DosyaListe()
        {
            List<DosyaModel> liste = db.Dosya.Select(x => new DosyaModel()
            {
                dosyaId =x.dosyaId,
                dosyaAdi=x.dosyaAdi,
                dosyaTuru=x.dosyaTuru
             }


            ).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api//dosyabyid/{dosyaId}")]

         public DosyaModel DosyaById(string dosyaId)
        {
            DosyaModel kayit = db.Dosya.Where(s => s.dosyaId == dosyaId).Select
                (x => new DosyaModel()
                {
                    dosyaId = x.dosyaId,
                    dosyaAdi = x.dosyaAdi,
                    dosyaTuru = x.dosyaTuru
                }).SingleOrDefault();
            return kayit;

        }
        [HttpGet]
        [Route("api/dersekle")]
        public sonucModel DosyaEkle(DosyaModel model)
        {

            {
                if (db.Dosya.Count(s => s.dosyaAdi == model.dosyaAdi) > 0)
                    sonuc.islem = false;
                sonuc.mesaj = "Bu İsimde Dosya Mevcuttur";
                return sonuc;
            }

            Dosya yeni =  new Dosya();
            yeni.dosyaId = Guid.NewGuid().ToString();
            yeni.dosyaAdi = model.dosyaAdi;
            yeni.dosyaTuru = model.dosyaTuru;
            db.Dosya.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Dosya Yüklendi";
             return sonuc;
        }
        [HttpGet]
        [Route("api/dosyaduzenle")]
        public sonucModel DosyaDuzenle(DosyaModel model)
        {
            Dosya kayit = db.Dosya.Where(s => s.dosyaId == model.dosyaId).SingleOrDefault();
            if (kayit== null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Dosya Bulunamadı";
                return sonuc;
               

        }

            kayit.dosyaAdi = model.dosyaAdi;
            kayit.dosyaTuru = model.dosyaTuru;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Dosya Düzenlendi ";
            return sonuc;
        }
        [HttpGet]
        [Route("api/dosyasil/(dosyaId)")]
        public sonucModel DosyaSil(string  dosyaId)
        {
            Dosya kayit = db.Dosya.Where(s => s.dosyaId == dosyaId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Dosya Bulunamadı";
                return sonuc;


            }
            db.Dosya.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Dosya Silindi";
            return sonuc;
        }
        #endregion

        #region Uye

        [HttpGet]
        [Route("api/uyeliste")]
        public List<UyeModel> UyeListe()
        {
            List<UyeModel> liste = db.Uye.Select(x => new UyeModel()
            {
            uyeId=x.uyeId,
            uyeAdsoyad=x.uyeAdsoyad,
            uyeDogtarih=x.uyeDogtarih,
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public UyeModel UyeById(string uyeId)
        {
            UyeModel kayit = db.Uye.Where(s=>s.uyeId==uyeId).Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                uyeAdsoyad = x.uyeAdsoyad,
                uyeDogtarih = x.uyeDogtarih,
            }).SingleOrDefault();
            return kayit;
        }
        [HttpGet]
        [Route("api/Uyeekle")]
        public sonucModel UyeEkle(UyeModel model)
        {
            if (db.Uye.Count(s =>s.uyeAdsoyad==model.uyeAdsoyad)>0)
            {
                sonuc.islem = false;
                sonuc.mesaj="Girilen Ad Soyad Bulunmaktadır Başka Bir Ad Deneyiniz "
            }
            Uye yeni = new Uye();
            yeni.uyeAdsoyad = Guid.NewGuid().ToString();
            yeni.uyeDogtarih = model.uyeDogtarih;
            yeni.uyeId = model.uyeId;
            db.Uye.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Eklendi";
            return sonuc;
        }
        [HttpGet]
        [Route("api/uyesil")]
        public sonucModel UyeSil(string uyeId)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == uyeId).SingleOrDefault();
            if (kayit== null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            db.Uye.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Silindi ";
            return sonuc;
        }









        #endregion

    }
}
