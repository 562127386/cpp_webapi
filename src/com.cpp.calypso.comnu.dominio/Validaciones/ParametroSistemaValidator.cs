using FluentValidation;
using FluentValidation.Results;
using System;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Validaciones para la entidad Parametros
    /// </summary>
    public class ParametroSistemaValidator : AbstractValidator<ParametroSistema>
    {
        public ParametroSistemaValidator()
        {
            Custom(parametro =>
            {
                if (parametro.Tipo == TipoParametro.Numero)
                {
                    //TODO: Revisar si el el tipo es un double para soportar cualquier numero
                    double? valorConvertido = null;
                    try
                    {
                        valorConvertido = Convert.ToDouble(parametro.Valor);
                    }
                    catch (Exception ex)
                    {
                        if (!valorConvertido.HasValue)
                            return new ValidationFailure("Valor", "El valor debe ser numérico");
                    }
                 }

                if (parametro.Tipo == TipoParametro.Booleano)
                {
                    bool? valorConvertido = null;
                    try
                    {
                        valorConvertido = Convert.ToBoolean(parametro.Valor);
                    }
                    catch (Exception ex)
                    {
                        if (!valorConvertido.HasValue)
                            return new ValidationFailure("Valor", "El valor debe ser booleano");
                    }
                }

                if (parametro.Tipo == TipoParametro.Fecha)
                {
                    DateTime? valorConvertido = null;
                    try
                    {
                        valorConvertido = Convert.ToDateTime(parametro.Valor);
                    }
                    catch (Exception ex)
                    {
                        if (!valorConvertido.HasValue)
                            return new ValidationFailure("Valor", "El valor debe ser una fecha");
                    }
                }
                
              return null;
            });

        }

    }
}
