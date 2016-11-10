package com.example.admin.pruebatracking.Mensajes;

import java.util.ArrayList;

/**
 * Created by Mario on 18/10/2016.
 */
public class MsgConexion extends Mensaje{

    private String Mensaje;

    public MsgConexion(ArrayList<String> to, String from, String fecha, String mensaje) {
        super(to, from, fecha, "MsgConexion", true);
        this.Mensaje = mensaje;
    }

    public String getMensaje() {
        return Mensaje;
    }

    public void setMensaje(String mensaje) {
        this.Mensaje = mensaje;
    }
}
