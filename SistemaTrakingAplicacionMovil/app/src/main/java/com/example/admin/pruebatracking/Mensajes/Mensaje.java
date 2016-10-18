package com.example.admin.pruebatracking.Mensajes;

/**
 * Created by Mario on 18/10/2016.
 */
public class Mensaje {
    private String to;
    private String from;
    private String fecha;

    public Mensaje(String to, String from, String fecha, String tipo) {
        this.to = to;
        this.from = from;
        this.tipo = tipo;
        this.fecha = fecha;
    }

    private String tipo;

    public String getTo() {
        return to;
    }

    public void setTo(String to) {
        this.to = to;
    }

    public String getFrom() {
        return from;
    }

    public void setFrom(String from) {
        this.from = from;
    }

    public String getTipo() {
        return tipo;
    }

    public void setTipo(String tipo) {
        this.tipo = tipo;
    }




}
