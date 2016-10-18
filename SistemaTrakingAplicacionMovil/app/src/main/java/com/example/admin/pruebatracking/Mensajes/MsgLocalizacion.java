package com.example.admin.pruebatracking.Mensajes;

/**
 * Created by Mario on 18/10/2016.
 */
public class MsgLocalizacion extends Mensaje {



    public MsgLocalizacion(String to, String from, String fecha, String latitud, String longitud) {
        super(to, from, fecha, "MsgLocalizacion");
        this.latitud = latitud;
        this.longitud = longitud;

    }

    private String latitud;
    private String longitud;

    public String getLatitud() {
        return latitud;
    }

    public void setLatitud(String latitud) {
        this.latitud = latitud;
    }

    public String getLongitud() {
        return longitud;
    }

    public void setLongitud(String longitud) {
        this.longitud = longitud;
    }



}
