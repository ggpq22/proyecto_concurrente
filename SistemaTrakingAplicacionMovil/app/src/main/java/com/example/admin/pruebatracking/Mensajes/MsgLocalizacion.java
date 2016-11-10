package com.example.admin.pruebatracking.Mensajes;

import java.util.ArrayList;

/**
 * Created by Mario on 18/10/2016.
 */
public class MsgLocalizacion extends Mensaje {

    public MsgLocalizacion(ArrayList<String> to, String from, String fecha, String latitud, String longitud) {
        super(to, from, fecha, "MsgLocalizacion", true);
        this.Latitud = latitud;
        this.Longitud = longitud;

    }

    private String Latitud;
    private String Longitud;

    public String getLatitud() {
        return Latitud;
    }

    public void setLatitud(String latitud) {
        this.Latitud = latitud;
    }

    public String getLongitud() {
        return Longitud;
    }

    public void setLongitud(String longitud) {
        this.Longitud = longitud;
    }



}
