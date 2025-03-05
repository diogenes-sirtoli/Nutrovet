using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class DominiosBll
    {
        public enum ListarAnimaisPor
        {
            Cliente = 1,
            Tutor
        }

        public enum Sexo
        {
            Macho = 1,
            Fêmea
        }

        public enum Periodo
        {
            Mensal = 1,
            Anual
        }

        public enum AcessosPlanosAuxSituacao
        {
            Avaliação = 1,
            Pago,
            Permitido,
            Cancelado,
            Pagamento_Pendente,
            Não_Pago,
            Encerrado
        }

        public enum AcessosPlanosAuxSituacaoIngles
        {
            Trialing = 1,
            Paid,
            Allowed,
            Canceled,
            PendingPayment,
            Unpaid,
            Ended
        }

        public enum StatusTransacao
        {
            Aguardando_Pagamento = 1,
            Em_Análise,
            Pago,
            Disponível,
            Em_Disputa,
            Devolvido,
            Cancelado
        }

        public enum Unidades
        {
            µg = 1,
            g,
            mg,
            Kcal,
            UI,
            Proporção,
            mcg_Kg,
            mg_animal,
            mg_Kg,
            UI_Kg,
        }

        public enum DietasAlimentosRecomendacao
        {
            Indicado = 1,
            Contraindicado
        }

        public enum ExigenciasNutrAuxTabelas
        {
            FEDIAF = 1,
            NRC,
            AAFCO
        }

        public enum ExigenciasNutrAuxTipoValor
        {
            Mínimo = 1,
            Máximo,
            Adequado,
            Recomendado
        }

        public enum PessoasAuxTipos
        {
            Administrador_do_Sistema = 1,
            Cliente,
            Tutor
        }

        public enum ReceitasAuxTipos
        {
            Suplementação = 1,
            Nutracêuticos,
            Receita_em_branco
        }

        public enum PessoasEntidadesAuxTipos
        {
            Pessoa_Física = 1,
            Pessoa_Jurídica
        }

        public enum ExigenciasNutrAuxIndicacoes
        {
            Adulto = 1,
            CresInicial,
            CresFinal,
            Gestante,
            Lactante
        }

        public enum CupomDescontoTipos
        {
            Percentual = 1,
            Valor,
            Mensal,
            Anual,
            Percentual_Mensal
        }

        public enum PortalContatoAuxSituacao
        {
            Enviada = 1,
            Recebida,
            Lida,
            Respondida,
            Arquivada
        }

        public enum PlanosAuxTipos
        {
            Plano_Principal = 1,
            Módulo_Complementar,
            Voucher_30_Dias_e_5Percent_Desconto,
            Voucher_30_Dias_Gratuíto,
            Voucher_5Percent_Desconto
        }

        public enum PlanosAuxNomes
        {
            Básico = 1,
            Intermediário,
            Completo,
            Receituário,
            Prontuário
        }

        public enum CartaoCreditoAuxBandeiras
        {
            Visa = 1,
            Master,
            Dinners,
            Elo,
            HiperCard,
            American_Express
        }

        public enum CartaoCreditoAuxBandeirasPagarMe
        {
            visa = 1,
            mastercard,
            dinners,
            elo,
            hiper,
            amex
        }

        public enum PlanosAuxEmpresaPgto
        {
            Paypal = 1,
            PagSeguro,
            PargarMe,
            MercadoPago
        }

        public enum PessoasAuxNacionalidade
        {
            Brasileira = 1,
            Estrangeira
        }

        public enum TelefonesAuxTipos
        {
            Fixo = 1,
            Celular
        }

        public enum AcoesCrud
        {
            Inserir = 1,
            Alterar,
            Excluir,
            Carregar,
            Consultar,
            Listar,
            Gerar,
            Gerar_Relatorio,
            Efetuar_Logon
        }

        public enum LogTabelas
        {
            Acessos = 1,
            AcessosAuxFuncoes,
            AcessosAuxTelas,
            AcessosFuncoesTelas,
            AcessosVigenciaCupomDesconto,
            AcessosVigenciaPlanos,
            AcessosVigenciaSituacao,
            AlimentoNutrientes,
            Alimentos,
            AlimentosAuxCategorias,
            AlimentosAuxFontes,
            AlimentosAuxGrupos,
            Animais,
            AnimaisAuxEspecies,
            AnimaisAuxRacas,
            AnimaisPesoHistorico,
            Biblioteca,
            BibliotecaAuxSecoes,
            Cardapio,
            CardapiosAlimentos,
            ConfigReceituario,
            Dietas,
            DietasAlimentos,
            ExigenciasNutrAuxIndicacoes,
            ExigenciasNutricionais,
            Nutraceuticos,
            Nutrientes,
            NutrientesAuxGrupos,
            Pessoas,
            PessoasCartaoCredito,
            PessoasDocumentos,
            PlanosAssinaturas,
            PortalContato,
            PrescricaoAuxTipos,
            Tutores
        }
    }
}
