﻿namespace Otel_Rezervasyon_Sistemi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB13 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fiyat_Tablo",
                c => new
                    {
                        FiyatID = c.Int(nullable: false, identity: true),
                        FiyatTipi = c.String(nullable: false, maxLength: 4000),
                        FiyatOran = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.FiyatID);
            
            CreateTable(
                "dbo.KayitliUye_Tablo",
                c => new
                    {
                        KayitliUyeID = c.Int(nullable: false, identity: true),
                        Mail = c.String(nullable: false, maxLength: 100),
                        Sifre = c.String(nullable: false, maxLength: 16),
                    })
                .PrimaryKey(t => t.KayitliUyeID);
            
            CreateTable(
                "dbo.Musteri_Tablo",
                c => new
                    {
                        MusteriId = c.Int(nullable: false, identity: true),
                        OdaID = c.Int(nullable: false),
                        KimlikNo = c.String(nullable: false, maxLength: 11),
                        Ad = c.String(nullable: false, maxLength: 30),
                        Soyad = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.MusteriId)
                .ForeignKey("dbo.Oda_Tablo", t => t.OdaID, cascadeDelete: true)
                .Index(t => t.OdaID);
            
            CreateTable(
                "dbo.Oda_Tablo",
                c => new
                    {
                        OdaID = c.Int(nullable: false, identity: true),
                        OdaNumarasi = c.String(maxLength: 4000),
                        KisiSayisi = c.Int(nullable: false),
                        BosMu = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OdaID);
            
            CreateTable(
                "dbo.Rezervasyon_Tablo",
                c => new
                    {
                        RezervasyonID = c.Int(nullable: false, identity: true),
                        KayitliUyeID = c.Int(nullable: false),
                        RezervasyonTipiID = c.Int(nullable: false),
                        GirisTarihi = c.DateTime(nullable: false),
                        CikisTarihi = c.DateTime(nullable: false),
                        KisiSayisi = c.Int(nullable: false),
                        OdaSayisi = c.Int(nullable: false),
                        YatakSayisi = c.Int(nullable: false),
                        Fiyat = c.Decimal(nullable: false, storeType: "money"),
                        OdaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RezervasyonID)
                .ForeignKey("dbo.KayitliUye_Tablo", t => t.KayitliUyeID, cascadeDelete: true)
                .ForeignKey("dbo.Oda_Tablo", t => t.OdaID, cascadeDelete: true)
                .ForeignKey("dbo.RezervasyonTipi_Tablo", t => t.RezervasyonTipiID, cascadeDelete: true)
                .Index(t => t.KayitliUyeID)
                .Index(t => t.RezervasyonTipiID)
                .Index(t => t.OdaID);
            
            CreateTable(
                "dbo.RezervasyonTipi_Tablo",
                c => new
                    {
                        Tipi = c.String(nullable: false, maxLength: 30),
                        Acıklama = c.String(nullable: false, maxLength: 100),
                        RezervasyonTipiId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.RezervasyonTipiId);
            
            CreateTable(
                "dbo.RezervOda",
                c => new
                    {
                        RezervOdaID = c.Int(nullable: false, identity: true),
                        RezervasyonID = c.Int(nullable: false),
                        OdaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RezervOdaID)
                .ForeignKey("dbo.Oda_Tablo", t => t.OdaID, cascadeDelete: false)
                .ForeignKey("dbo.Rezervasyon_Tablo", t => t.RezervasyonID, cascadeDelete: false)
                .Index(t => t.RezervasyonID)
                .Index(t => t.OdaID);
            
            CreateTable(
                "dbo.Yonetici_Table",
                c => new
                    {
                        YoneticiID = c.Int(nullable: false, identity: true),
                        KullaniciAdi = c.String(nullable: false, maxLength: 100),
                        Sifre = c.String(nullable: false, maxLength: 16),
                    })
                .PrimaryKey(t => t.YoneticiID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RezervOda", "RezervasyonID", "dbo.Rezervasyon_Tablo");
            DropForeignKey("dbo.RezervOda", "OdaID", "dbo.Oda_Tablo");
            DropForeignKey("dbo.Rezervasyon_Tablo", "RezervasyonTipiID", "dbo.RezervasyonTipi_Tablo");
            DropForeignKey("dbo.Rezervasyon_Tablo", "OdaID", "dbo.Oda_Tablo");
            DropForeignKey("dbo.Rezervasyon_Tablo", "KayitliUyeID", "dbo.KayitliUye_Tablo");
            DropForeignKey("dbo.Musteri_Tablo", "OdaID", "dbo.Oda_Tablo");
            DropIndex("dbo.RezervOda", new[] { "OdaID" });
            DropIndex("dbo.RezervOda", new[] { "RezervasyonID" });
            DropIndex("dbo.Rezervasyon_Tablo", new[] { "OdaID" });
            DropIndex("dbo.Rezervasyon_Tablo", new[] { "RezervasyonTipiID" });
            DropIndex("dbo.Rezervasyon_Tablo", new[] { "KayitliUyeID" });
            DropIndex("dbo.Musteri_Tablo", new[] { "OdaID" });
            DropTable("dbo.Yonetici_Table");
            DropTable("dbo.RezervOda");
            DropTable("dbo.RezervasyonTipi_Tablo");
            DropTable("dbo.Rezervasyon_Tablo");
            DropTable("dbo.Oda_Tablo");
            DropTable("dbo.Musteri_Tablo");
            DropTable("dbo.KayitliUye_Tablo");
            DropTable("dbo.Fiyat_Tablo");
        }
    }
}
