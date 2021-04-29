using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Metodo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double _ARS = 0, _AFP = 0, _ISR = 0, _DEDUCCION = 0, _SUELDONETO = 0, _SALARIO = 0;

        claNomina Calculadora = new claNomina();
        private void btn_calcular_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txt_salario.Text,out _SALARIO)) {

                _SALARIO = double.Parse(txt_salario.Text);
                procesarCalculo();
                MostrarDatos();
            }
            else
            {
                MessageBox.Show("Debe insertar valor numerico");
                txt_salario.Clear();
                txt_salario.Focus();

            }
            
        }

        private void btn_nuevo_Click(object sender, EventArgs e)
        {
            Limpiar();            
        }

        //metodo limpiar componentes
        void Limpiar()
        {
            txt_nombre.Clear();
            txt_salario.Clear();
            txt_afp.Clear();
            txt_ars.Clear();
            txt_isr.Clear();
            txt_sueldoNeto.Clear();
            txt_deduccion.Clear();
            txt_nombre.Enabled = true;
            txt_salario.Enabled = true;
            txt_nombre.Focus();
        }
        //metodo ejecutar calculos
        void procesarCalculo()
        {
            _ARS = Calculadora.calculo_ARS(_SALARIO);
            _AFP = Calculadora.calculo_AFP(_SALARIO);
            _DEDUCCION = _ARS + _AFP;
            _ISR = Calculadora.calculo_ISR(_SALARIO, _DEDUCCION);

            if (_ISR != 0) { _DEDUCCION = _ARS + _AFP + _ISR; }

            _SUELDONETO = _SALARIO - _DEDUCCION - _ISR;
        }

        void MostrarDatos()
        {
            txt_nombre.Enabled = false;
            txt_salario.Enabled = false;
            txt_salario.Text = _SALARIO.ToString("###,###.##");
            txt_afp.Text = _AFP.ToString("###,###.##");
            txt_ars.Text = _ARS.ToString("###,###.##");
            txt_isr.Text = _ISR.ToString("###,###.##");
            txt_deduccion.Text = _DEDUCCION.ToString("###,###.##");
            txt_sueldoNeto.Text = _SUELDONETO.ToString("###,###.##");
        }
    }
    //creacion de la clase nomina
    class claNomina
    {
        //metodos de la clase nomina

       public double calculo_ARS(double salario)
        {
            return salario * 0.0304; //descuento ARS es el 3.04% del salario
        }

        public double calculo_AFP(double salario)
        {
            return salario * 0.0287; //descuento AFP es el 2.87% del salario
        }

       public double calculo_ISR(double salario, double deducciones)
        {
            double ISR = 0.00;
            double _IngresoAnual = (salario - deducciones) * 12;
            
            if (_IngresoAnual >= 416220.01 && _IngresoAnual <= 624329)
            {
                ISR = ((_IngresoAnual - 416220.01) * 0.15)/12;
            }
            else if (_IngresoAnual >= 624329.01 && _IngresoAnual <= 867123)
            {
                ISR = (((_IngresoAnual - 624329.01) * 0.20) + 31216) / 12;
            }
            else if (_IngresoAnual >= 867123.01)
            {
                ISR = (((_IngresoAnual - 624329.01) * 0.20) + 79776) / 12;
            }

            return ISR;

        }
    }
}
