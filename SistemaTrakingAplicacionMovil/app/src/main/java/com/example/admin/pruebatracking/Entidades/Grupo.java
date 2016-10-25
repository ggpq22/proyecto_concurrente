package com.example.admin.pruebatracking.Entidades;

import java.util.ArrayList;

/**
 * Created by Mario on 25/10/2016.
 */
public class Grupo {

    private int IdGrupo;

    private Cuenta Anfitrion;

    public ArrayList<Cuenta> Integrantes = new ArrayList<>();

    public Grupo() {
    }

    public Cuenta getAnfitrion() {
        return Anfitrion;
    }

    public void setAnfitrion(Cuenta anfitrion) {
        Anfitrion = anfitrion;
    }

    public int getIdGrupo() {
        return IdGrupo;
    }

    public void setIdGrupo(int idGrupo) {
        this.IdGrupo = idGrupo;
    }
}
