﻿using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unicell.API.Models;
using Unicell.DTO;

namespace Unicell.DAL
{
    public class DALConnectionMobile
    {
        public static string[] SendAcessos(string androidID, SendAcessosModel.Acesso[] packages)
        {
            return packages.Select(package => {
                Utils.DapperConnection.Query("MANTER_ACESSO", new
                {
                    ANDROID_ID = androidID,
                    PACKAGE_NAME = package.packageName,
                    ACESSO = DateTime.Parse(package.acesso)
                }, commandType: CommandType.StoredProcedure);

                return package.packageName;
            }).ToArray();
        }

        public static MobileResultDTO SendMobile(string ANDROID_ID, string GEO_LOCALIZACAO, string ANDROID_STATUS, string[] TELEFONES, bool ISCHARGING, int SIGNALSTRENGTH, float CHARGELEVEL)
        {
            var query = Utils.DapperConnection.QueryMultiple("MANTER_MOBILE", new
            {
                ANDROID_ID = ANDROID_ID,
                GEO_LOCALIZACAO = GEO_LOCALIZACAO,
                ANDROID_STATUS = ANDROID_STATUS,
                ISCHARGING = ISCHARGING,
                SIGNALSTRENGTH = SIGNALSTRENGTH,
                CHARGELEVEL = CHARGELEVEL
            }, commandType: CommandType.StoredProcedure);

            return new MobileResultDTO()
            {
                Autorizacoes = query.Read<MobileResultDTO.Autorizacao>().ToList(),
                Configuracao = query.ReadFirst<MobileResultDTO.Configuracoes>()
            };
        }

        public static void SendEmpresa(int ID_EMPRESA, string ANDROID_ID)
        {
            Utils.DapperConnection.Execute("UPDATE MOBILE SET ID_EMPRESA = @ID_EMPRESA WHERE ANDROID_ID = @ANDROID_ID", new
            {
                @ANDROID_ID = ANDROID_ID,
                @ID_EMPRESA = ID_EMPRESA
            });
        }

    }
}
