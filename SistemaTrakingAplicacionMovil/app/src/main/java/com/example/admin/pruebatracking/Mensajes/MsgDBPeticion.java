package com.example.admin.pruebatracking.Mensajes;

import android.app.Notification;

import com.example.admin.pruebatracking.Entidades.Cuenta;
import com.example.admin.pruebatracking.Entidades.DBEntidad;
import com.example.admin.pruebatracking.Entidades.Grupo;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Admin on 1/11/2016.
 */
public class MsgDBPeticion extends Mensaje {

    private String CodigoPeticion;
    private ArrayList<Cuenta> ParamsCuenta;
    private ArrayList<Grupo> ParamsGrupo;
    private boolean Notificacion;

    public MsgDBPeticion(ArrayList<String> to, String from, String fecha, String CodigoPeticion, ArrayList<Cuenta> ParamsCuenta, ArrayList<Grupo> ParamsGrupo, boolean Notificacion) {
        super(to, from, fecha, "MsgDBPeticion", true);
        this.CodigoPeticion = CodigoPeticion;
        this.ParamsCuenta = ParamsCuenta;
        this.ParamsGrupo = ParamsGrupo;
        this.Notificacion = Notificacion;
    }

    public String getCodigoPeticion() {
        return CodigoPeticion;
    }

    public void setCodigoPeticion(String CodigoPeticion) {
        this.CodigoPeticion = CodigoPeticion;
    }

    public ArrayList<Cuenta> getParamsCuenta() {
        return ParamsCuenta;
    }

    public void setParamsCuenta(ArrayList<Cuenta> ParamsCuenta) {
        this.ParamsCuenta = ParamsCuenta;
    }

    public ArrayList<Grupo> getParamsGrupo() {
        return ParamsGrupo;
    }

    public void setParamsGrupo(ArrayList<Grupo> ParamsGrupo) {
        this.ParamsGrupo = ParamsGrupo;
    }

}
