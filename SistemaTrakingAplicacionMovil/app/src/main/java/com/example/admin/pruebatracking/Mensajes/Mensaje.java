package com.example.admin.pruebatracking.Mensajes;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Mario on 18/10/2016.
 */
public class Mensaje {
    private String To;
    private String From;
    private String Fecha;
    private String Tipo;
    private boolean IsValido;
    public ArrayList<String> Errores = new ArrayList<>();

    public Mensaje(String to, String from, String fecha, String tipo, boolean IsValido) {
        this.To = to;
        this.From = from;
        this.Tipo = tipo;
        this.Fecha = fecha;
        this.IsValido = IsValido;
    }

    public String getFecha(){
        return Fecha;
    }

    public void setFecha(String fecha){
        this.Fecha = fecha;
    }

    public String getTo() {
        return To;
    }

    public void setTo(String to) {
        this.To = to;
    }

    public String getFrom() {
        return From;
    }

    public void setFrom(String from) {
        this.From = from;
    }

    public String getTipo() {
        return Tipo;
    }

    public void setTipo(String tipo) {
        this.Tipo = tipo;
    }

    public boolean getIsValido() {
        return IsValido;
    }

    public void setIsValido(boolean IsValido) {
        this.IsValido = IsValido;
    }




}
