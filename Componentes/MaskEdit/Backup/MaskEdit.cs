using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace MaskEdit
{
	/// <summary>
	/// Summary description for WebCustomControl1.
	/// </summary>
	[ToolboxData("<{0}:MEdit runat=server></{0}:MEdit>")]
	public class MEdit : System.Web.UI.WebControls.TextBox
	{
		private const string IdScript = "ScriptMascara";
		private StringCollection Java = new StringCollection();

        public enum TpMascara
        {
            AlfaNumerico, Boletim, PlanoContas, CEP, CPF, Data, Competencia, Pis, 
            String, Telefone, Titulo, CNPJ, Float, Inteiro, Hora, Money
        }
		public enum TpPeriodo {Nulo,Ano, Mes, Dia}
		public int QtdNr = 0;
		public DateTime TpDataIni;
		public DateTime TpDataFin;
		public bool DtMaior;

		TpMascara VMascara;
		TpPeriodo VPeriodo;
		DateTime VDataIni;
		DateTime VDataFin;
		
		public override int MaxLength
		{
			set
			{
				base.MaxLength = value;
			}
		}
		public TpMascara Mascara
		{
			get {return VMascara;}
			set {VMascara = value;}
		}
		public TpPeriodo Periodo
		{
			get {return VPeriodo;}
			set {VPeriodo = value;}
		}
		public int QtdValor
		{
			get {return QtdNr;}
			set {QtdNr = value;}
		}
		public DateTime DataIni
		{
			get {return VDataIni;}
			set {VDataIni = value;}
		}
		public DateTime DataFin
		{
			get {return VDataFin;}
			set {VDataFin = value;}
		}
		public bool DataMaior
		{
			get {return DtMaior;}
			set {DtMaior = value;}
		}
 
		protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
		{
			switch (Mascara)
			{
                case TpMascara.AlfaNumerico:
                    {
                        writer.AddAttribute("onkeypress", "return mascaraAlfa(this);");
                        break;
                    }
                case TpMascara.Boletim:
                    {
                        MaxLength = 9;
                        writer.AddAttribute("onkeypress", "mascaraBoletim(this);return Tecla(event);");
                        DesativaValor();
                        break;
                    }
                case TpMascara.PlanoContas:
                    {
                        MaxLength = 12;
                        writer.AddAttribute("onkeypress", "mascaraPlanoContas(this);return Tecla(event);");
                        DesativaValor();
                        break;
                    }
                case TpMascara.CEP:
                    {
                        MaxLength = 9;
                        writer.AddAttribute("onkeypress", "mascaraCep(this);return Tecla(event);");
                        DesativaValor();
                        break;
                    }
                case TpMascara.CPF:
                    {
                        writer.AddAttribute("onkeypress", "mascara_cpf(this);return Tecla(event);");
                        writer.AddAttribute("onchange", "ValidaCPF(this);");
                        MaxLength = 14;
                        DesativaValor();
                        break;
                    }
                case TpMascara.Telefone:
                    {
                        writer.AddAttribute("onkeypress", "mascaraTelefone(this);return Tecla(event);");
                        MaxLength = 14;
                        DesativaValor();
                        break;
                    }
                case TpMascara.Data:
                    {
                        writer.AddAttribute("onkeypress", "mascaraData(this);return Tecla(event)");
                        writer.AddAttribute("onchange", "ValidaData(this,'" + DataMaior + "');");
                        MaxLength = 10;
                        DefineValor();
                        break;
                    }
                case TpMascara.Competencia:
                    {
                        writer.AddAttribute("onkeypress", "mascaraCompetencia(this);return Tecla(event)");
                        writer.AddAttribute("onchange", "ValidaCompetencia(this);");
                        MaxLength = 7;
                        DesativaValor();
                        break;
                    }
                case TpMascara.CNPJ:
                    {
                        writer.AddAttribute("onkeypress", "mascaraCNPJ(this);return Tecla(event)");
                        MaxLength = 18;
                        DesativaValor();
                        break;
                    }
                case TpMascara.Pis:
                    {
                        writer.AddAttribute("onkeypress", "mascaraPis(this);return Tecla(event)");
                        MaxLength = 14;
                        DesativaValor();
                        break;
                    }
                case TpMascara.Titulo:
                    {
                        writer.AddAttribute("onkeypress", "mascaraTitulo(this);return Tecla(event)");
                        MaxLength = 15;
                        DesativaValor();
                        break;
                    }
                case TpMascara.String:
                    {
                        writer.AddAttribute("onkeypress", "mascaraString(this)");
                        DesativaValor();
                        break;
                    }
                case TpMascara.Float:
                    {
                        writer.AddAttribute("onkeypress",
                            "mascaraFloat(this);return TeclaFloat(event);");
                        DesativaValor();
                        break;
                    }
                case TpMascara.Money:
                    {
                        writer.AddAttribute("onkeypress",
                            "mascaraMoney(this);return TeclaFloat(event);");
                        DesativaValor();
                        break;
                    }
                case TpMascara.Inteiro:
                    {
                        writer.AddAttribute("onkeypress",
                            "mascaraInteiro(this);return Tecla(event);");
                        DesativaValor();
                        break;
                    }
                case TpMascara.Hora:
                    {
                        MaxLength = 5;
                        writer.AddAttribute("onKeypress",
                            "mascaraHora(this);return Tecla(event);");
                        writer.AddAttribute("onchange", "ValidaHora(this);");
                        DesativaValor();
                        break;
                    }
			}
			
			base.AddAttributesToRender(writer);
		}

		protected override void OnPreRender(System.EventArgs e)
		{
			DefineValor();
			if (!this.Page.IsClientScriptBlockRegistered(IdScript))
			{
				//Mascaras
				Page.RegisterClientScriptBlock(IdScript,
					"<script> " +
                    @"function mascaraAlfa(alfa)
                      {var tecla = event.keyCode; 
                       if ((tecla > 47 && tecla < 58) || (tecla > 64 && tecla < 91) || 
                           (tecla > 96 && tecla < 123))
                       {
                            return true;
                       } 
                       else 
                       {
                            if ((tecla == 13) || (tecla == 8)) 
                                return true; 
                            else 
                                return false;
                       }
                      } " +
                    "function mascaraBoletim(bol){var mybol = ''; mybol = mybol + bol.value; if (mybol.length == 4){mybol = mybol + '/'; bol.value = mybol;} if (mybol.length == 10){}} " +
                    "function mascaraPlanoContas(PC){var myPC = ''; myPC = myPC + PC.value; if (myPC.length == 1){myPC = myPC + '.'; PC.value = myPC;} if (myPC.length == 3){myPC = myPC + '.'; PC.value = myPC;} if (myPC.length == 5){myPC = myPC + '.'; PC.value = myPC;} if (myPC.length == 8){myPC = myPC + '.'; PC.value = myPC;} if (myPC.length == 12){}} " +
					"function mascaraCep(cep){var mycep = ''; mycep = mycep + cep.value; if (mycep.length == 5){mycep = mycep + '-'; cep.value = mycep;} if (mycep.length == 10){}} " +
					@"function Tecla(e)
                      {
                        var tecla = event.keyCode; 
                        if (tecla > 47 && tecla < 58)
                        {
                            return true;
                        } 
                        else
                        {
                            if (tecla != 8) 
                                return false; 
                            else 
                                return true;
                        }
                    } " +
                    @"function TeclaFloat(e)
                      {
                        var tecla = event.keyCode; 
                        
                        switch(tecla)
                        { 
                            case 8:
                            case 44:
                            case 48: 
                            case 49: 
                            case 50: 
                            case 51: 
                            case 52: 
                            case 53: 
                            case 54: 
                            case 55: 
                            case 56: 
                            case 57: 
                            {
                                return true;
                                break; 
                            }
                            default:
                            {
                                return false;
                                break; 
                            }
                        }
                    } " +
					"function mascaraTelefone(fone){var myfone = ''; myfone = myfone + fone.value; if (myfone.length == 0){myfone = myfone + '('; fone.value = myfone;} if (myfone.length == 3){myfone = myfone + ')'; fone.value = myfone;} if (myfone.length == 4){myfone = myfone + ' '; fone.value = myfone;} if (myfone.length == 9){myfone = myfone + '-'; fone.value = myfone;} if (myfone.length == 14){}} " +
					"function mascara_cpf(cpf){var mycpf = ''; mycpf = mycpf + cpf.value; if (mycpf.length == 3){mycpf = mycpf + '.'; cpf.value = mycpf;} if (mycpf.length == 7){mycpf = mycpf + '.'; cpf.value = mycpf;} if (mycpf.length == 11){mycpf = mycpf + '-'; cpf.value = mycpf;} if (mycpf.length == 14){}} " +
                    "function mascaraCompetencia(compet){var myCompet = ''; myCompet = myCompet + compet.value; if (myCompet.length == 2){myCompet = myCompet + '/'; compet.value = myCompet;} if (myCompet.length == 7){}} " +
                    "function mascaraCNPJ(cnpj){var mycnpj = ''; mycnpj = mycnpj + cnpj.value; if (mycnpj.length == 2){mycnpj = mycnpj + '.'; cnpj.value = mycnpj;} if (mycnpj.length == 6){mycnpj = mycnpj + '.'; cnpj.value = mycnpj;} if (mycnpj.length == 10){mycnpj = mycnpj + '/'; cnpj.value = mycnpj;} if (mycnpj.length == 15){mycnpj = mycnpj + '-'; cnpj.value = mycnpj;} if (mycnpj.length == 18){}} " +
					"function mascaraPis(pis){var mypis = ''; mypis = mypis + pis.value; if (mypis.length == 3){mypis = mypis + '.'; pis.value = mypis;} if (mypis.length == 9){mypis = mypis + '.'; pis.value = mypis;} if (mypis.length == 12){mypis = mypis + '.'; pis.value = mypis;} if (mypis.length == 14){}} " +
					"function mascaraTitulo(titulo){var mytitulo = ''; mytitulo = mytitulo + titulo.value; if (mytitulo.length == 3){mytitulo = mytitulo + '.'; titulo.value = mytitulo;} if (mytitulo.length == 7){mytitulo = mytitulo + '.'; titulo.value = mytitulo;} if (mytitulo.length == 12){mytitulo = mytitulo + '/'; titulo.value = mytitulo;} if (mytitulo.length == 15){}} " +
					"function mascaraString(string){} " +
					"function mascaraData(data){var mydata = ''; mydata = mydata + data.value; if (mydata.length == 2){mydata = mydata + '/'; data.value = mydata;} if (mydata.length == 5){mydata = mydata + '/'; data.value = mydata;} if (mydata.length == 10){}} " +
					"function mascaraInteiro(Inteiro){} " +
                    "function mascaraFloat(Float){} " +
					"function mascaraHora(hora){var myhora = ''; myhora = myhora + hora.value; if (myhora.length == 2){myhora = myhora + ':'; hora.value = myhora;} if (myhora.length == 5){}} " + 

					// Validações
					"function ValidaCPF(scpf) " +
					"{ " +
					"	var cpf = trimtodigits(scpf.value); " +					
					"	var soma, resto; " +
					"   var MsgErro = 'CPF inválido'; " + 
					"	var multiplicador1 = new Lista(10, 9,  8, 7, 6, 5, 4, 3, 2);  " +
					"	var multiplicador2 = new Lista(11, 10, 9, 8, 7, 6, 5, 4, 3, 2);  " +
					"   obj = document.getElementById(scpf.name);" + 
					
					"	if (cpf.length != 11) " +
					"	{ " +
					"		alert(MsgErro);"+
					"       obj.value = \"\"; " + 
					"		return false; " +
					"	} " +	
					
					"	soma = 0; " +
					"	for(i=0; i<9; i++) " +
					"		soma += cpf.charAt(i) * multiplicador1[i]; " +

					"	resto = soma % 11; " +	
					"	if (resto < 2 ) " +
					"		resto = 0; " +
					"	else " +
					"		resto = 11 - resto; " +
						
					"	if(cpf.charAt(9)!= resto) " +
					"	{ " +
					"		alert(MsgErro);"+
					"       obj.value = \"\"; " + 
					"		return false; " +
					"	} " +	
					
					"	soma = resto * multiplicador2[9]; " +
					"	for(i=0; i<9; i++) " +
					"		soma += cpf.charAt(i) * multiplicador2[i]; " +
							
					"	resto = soma % 11; " +
					"	if (resto < 2) " +
					"		resto = 0; " +
					"	else " +
					"		resto = 11 - resto; " +

					"	if(cpf.charAt(10)!= resto) " +
					"	{ " +
					"		alert(MsgErro);"+
					"       obj.value = \"\"; " + 
					"		return false; " +
					"	} " +	

					"	return true; " +
					"} " +
										
					// retorna somente dígitos numéricos
					"function trimtodigits(ts) " +
					"{ " +
					"	var s=''; " +
					"	for (x=0;x<ts.length;x++) " +
					"	{ " +
					"		ch = ts.charAt(x); " +
					"		if (asc(ch)>=48 && asc(ch)<=58) " +
					"			s = s + ch; " +
					"	} " +
					"	return s; " +
					"} " +			
					
					// Classe lista utilizada em arrays
					"function Lista() " +
					"{ " +
					"	var args = Lista.arguments; " +	

					"	this.length = args.length; " +
					"	for (i = 0; i < (args.length); i += 1) " +
					"	{ " +
					"		this[i] = args[i]; " + 
					"	} " +
					"} " +

					// Retorna o código ASC do caracter passada por parâmetro " 
					"function asc(achar) " +
					"{ " +
					"	var n=0; " +
					"	var ascstr = makeCharsetString(); " +  
					"	for(i=0;i<ascstr.length;i++) " +
					"	{ " +
					"		if(achar==ascstr.substring(i,i+1)) " +
					"		{ " +
					"			n=i; " +
					"			break; " +
					"		} " +
					"	} " +
					"	return n+32; " +
					"} " +

					// Gera uma string com os caracteres básicos na sequência de códigos ASC " 
					"function makeCharsetString() " +
					"{ " +
					"	var astr; " +
					"	astr = ' !_#$%&__()*+,-./0123456789:;<=>?@'; " +
					"	astr+= 'ABCDEFGHIJKLMNOPQRSTUVWXYZ'; " +
					"	astr+= '[_]^_`abcdefghijklmnopqrstuvwxyz'; " +
					"	astr+= '{|}~'; " +
					"	return astr; " +
					"} " +  
									
					// valida hora retorna se errada
					"function ValidaHora(vhora) "+
					"{ " +
					"	hrs = (vhora.value.substring(0,2)); " +
					"	min = (vhora.value.substring(3,5)); " + 
					"	if((hrs.length == 2)&&(min.length == 2)) " +
					"	{ " +
					"		if ((hrs < 00 ) || (hrs > 23) || ( min < 00) ||( min > 59)) " +
					"		{ " +
					"			alert('Hora inválida'); " +
					"			vhora.value = ''; " +
					"		} " +
					"	} " + 
					"	else " +
					"	{ " +
					" 		alert('Hora inválida'); " +
					"		vhora.value = ''; " +
					"	} " +
					"} " +					
					
					// valida data retorna se errada
					"function ValidaData(vdata, vdataMaior) " +
					"{ " +
					"	var diaDig = parseInt(vdata.value.substring(0,2)); " +
					"	var mesDig = parseInt(vdata.value.substring(3,5)); " +					
					"	var anoDig = parseInt(vdata.value.substring(6,10)); " +
					"	var dtMaior = vdataMaior; " +
					"	if(diaDig == 0) " +
					"	{ " +
					"		var diaDig = parseInt(vdata.value.substring(1,2)); " +
					"	} " +
					"	if(mesDig == 0) " +
					"	{ " +
					"		var mesDig = parseInt(vdata.value.substring(4,5)); " +
					"	} " +

					"	var datadigitada = new Date(anoDig,(mesDig-1),diaDig); " +
					"	var miliqq = datadigitada.getTime(); " +

					"	var mydate = new Date(); " +
					"	var mili = mydate.getTime(); " +

					"	var diaqq = parseInt(datadigitada.getDate()); " +
					"	var mesqq = parseInt(datadigitada.getMonth())+1; " +
					
					"	if(anoDig <= 1900) " +
					"	{ " +
					"		alert('Informe Data superior ao ano 1900'); " +
					"		vdata.value = ''; " +
					"	} " +
					"	else if((diaDig != diaqq) || (mesDig != mesqq)) " +
					"	{ " +
					"		alert('Data inválida'); " +
					"		vdata.value = ''; " +
					"	} " +
					//Testa se a Data Digitada é maior que data atual
					"	else if(miliqq > mili) " +
					"	{ " +
					"		if(dtMaior != 'True') " +
					"		{ " +
					"			alert('Data Digitada maior que data atual'); " +
					"			vdata.value = ''; " +
					"		} " +
					"	} " +					
					//Testa e Valida a Data conforme os Limitadores definidos
					//pelas propriedades: DataIni e DataFin
					"	if("+QtdValor+" > 0) " +
					"	{ " +
					"		var Dtdata = miliqq; " +

					"		var diaI = parseInt("+DataIni.Day+"); " +
					"		var mesI = parseInt("+DataIni.Month+"); " +
					"		var anoI = parseInt("+DataIni.Year+"); " +
						
					"		var dataSysIni = new Date(anoI,(mesI-1),diaI); " +
					"		var DtIni = dataSysIni.getTime(); " +

					"		var diaF = parseInt("+DataFin.Day+"); " +
					"		var mesF = parseInt("+DataFin.Month+"); " +
					"		var anoF = parseInt("+DataFin.Year+"); " +

					"		var dataSysFin = new Date(anoF,(mesF-1),diaF); " +
					"		var DtFin = dataSysFin.getTime(); " +

					"		if((Dtdata < DtIni) || (Dtdata > DtFin)) " +
					"		{ " +
					"			alert('Data informada deve estar no intervalo de " + 
								DataIni.ToString("dd/MM/yyyy") + " à " + 
								DataFin.ToString("dd/MM/yyyy") + "'); " +
					"			vdata.value = ''; " +
					"		} " +
					"	} " +

					"} " +

					// Valida CompetÊncia
					"function ValidaCompetencia(comp) " +
					"{ " +					
					"   var mesDig = comp.value.substring(0,2); " +					
					"	var anoDig = comp.value.substring(3,7); " +
                    "	if (!((mesDig >= 1) && (mesDig <= 12))) " +
					"	{ " +
					"		alert('Mês de Competência inválido!!!');" +
                    "		comp.value = ''; " +
					"	} " +
                    "	else if (!((anoDig >= 1990) && (anoDig <= 2070))) " +
                    "	{ " +
                    "		alert('Ano de Competência inválido!!!');" +
                    "		comp.value = ''; " +
                    "	} " +
					"} " +
					"</script>");
			}
		}
		
		public void DesativaValor()
		{
			DataIni = Convert.ToDateTime(null);
			DataFin = Convert.ToDateTime(null);
			Periodo = MEdit.TpPeriodo.Nulo;
			QtdValor = 0;
		}
		
		public void DefineValor()
		{
			if((QtdValor > 0)&&(Periodo != MEdit.TpPeriodo.Nulo))
			{
				if ((DataIni != Convert.ToDateTime(null)) && 
					(DataFin == Convert.ToDateTime(null))) 
				{
					DataFin = Convert.ToDateTime(RetornoDtCalc(DataIni,"Fim"));
				}			
				if ((DataIni == Convert.ToDateTime(null)) && 
					(DataFin != Convert.ToDateTime(null))) 
				{
					DataIni = Convert.ToDateTime(RetornoDtCalc(DataFin,"Ini"));
				}
			}
		}

		private string RetornoDtCalc(DateTime DtValor, string DtTp)
		{
			string _DtCalc;
			int _diaF, _mesF, _anoF;

			_diaF = Convert.ToInt32(DtValor.Day);
			_mesF = Convert.ToInt32(DtValor.Month);
			_anoF = Convert.ToInt32(DtValor.Year);

			switch (Periodo)
			{				
				case MEdit.TpPeriodo.Ano:
				{					
					if(DtTp == "Fim")
						_anoF = _anoF + QtdValor;
					else
						_anoF = _anoF - QtdValor;
					break;
				}
				case MEdit.TpPeriodo.Mes:
				{					
					if(DtTp == "Fim")
						_mesF = _mesF + QtdValor;
					else
						_mesF = _mesF - QtdValor;
					break;
				}
				case MEdit.TpPeriodo.Dia:
				{					
					if(DtTp == "Fim")
						_diaF = _diaF + QtdValor;
					else
						_diaF = _diaF - QtdValor;
					break;
				}
			}
			_DtCalc = ""+ _diaF +"/"+ _mesF +"/"+ _anoF +"";
			return _DtCalc;
		}				
	}
}
