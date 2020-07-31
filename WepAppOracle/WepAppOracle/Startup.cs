using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oracle.ManagedDataAccess.Client;


namespace WepAppOracle
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    //Teste Usando ODP.NET Core

                    //Criando a conexão Oracle
                    string conString = "User Id=marte;Password=marte;" +
                    // Conectar no Oracle sem o uso do TNSNAMES.ORA
                    "Data Source=brdc1svdlo007.latam2.prosegur.local:1521/BRODSP01";

                    // Se eu quiser usar o TNSNAMES.ORA 
                    // "Data Source=<service name alias>"

                    using (OracleConnection con = new OracleConnection(conString))
                    {
                        using (OracleCommand cmd = con.CreateCommand())
                        {
                            try
                            {
                                con.Open();
                                cmd.BindByName = true;

                                cmd.CommandText = "select des_nombre_fantasia from copr_tcliente where bol_activo = :id";

                                OracleParameter id = new OracleParameter("id", 1);
                                cmd.Parameters.Add(id);

                                OracleDataReader reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    await context.Response.WriteAsync($"Nombre del Cliente: {reader.GetString(0)}\n");
                                }
                                reader.Dispose();

                            }
                            catch (Exception ex)
                            {
                                await context.Response.WriteAsync(ex.Message);
                                throw;
                            }
                        }
                    }

                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
