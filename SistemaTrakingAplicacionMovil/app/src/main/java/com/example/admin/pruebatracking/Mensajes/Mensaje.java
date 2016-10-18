package com.example.admin.pruebatracking.Mensajes;

/**
 * Created by Mario on 18/10/2016.
 */
public class Mensaje {
    private String to;
    private String from;

    public Mensaje(String to, String from, String tipo) {
        this.to = to;
        this.from = from;
        this.tipo = tipo;
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
