package com.example.admin.pruebatracking.Mensajes;

import com.example.admin.pruebatracking.Entidades.Cuenta;
import com.example.admin.pruebatracking.Entidades.DBEntidad;
import com.example.admin.pruebatracking.Entidades.Grupo;
import com.example.admin.pruebatracking.Entidades.Historial;

import java.util.ArrayList;

/**
 * Created by Admin on 1/11/2016.
 */
public class MsgDBRespuesta extends Mensaje {

    private String CodigoPeticion;
    private ArrayList<Cuenta> ReturnCuenta = new ArrayList<Cuenta>();
    private ArrayList<Grupo> ReturnGrupo = new ArrayList<Grupo>();
    private ArrayList<Historial> ReturnHistorial = new ArrayList<Historial>();

    public MsgDBRespuesta(ArrayList<String> to, String from, String fecha) {
        super(to, from, fecha, "MsgDBRespuesta", true);
    }

    public String getCodigoPeticion() {
        return CodigoPeticion;
    }

    public void setCodigoPeticion(String CodigoPeticion) {
        this.CodigoPeticion = CodigoPeticion;
    }
}
