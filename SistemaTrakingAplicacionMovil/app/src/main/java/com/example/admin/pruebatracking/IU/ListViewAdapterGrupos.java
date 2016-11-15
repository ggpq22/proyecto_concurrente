package com.example.admin.pruebatracking.IU;

import android.content.Context;
import android.graphics.Color;
import android.util.SparseArray;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.example.admin.pruebatracking.Entidades.Grupo;
import com.example.admin.pruebatracking.R;

import java.util.ArrayList;
import java.util.List;

public class ListViewAdapterGrupos extends BaseAdapter {

    Context context;
    ArrayList<Grupo> grupos;
    ArrayList<Boolean> seleccionados;
    LayoutInflater inflater;

    public ListViewAdapterGrupos(Context context, ArrayList<Grupo> grupos, ArrayList<Boolean> seleccionados) {
        this.context = context;
        this.grupos = grupos;
        this.seleccionados = seleccionados;
    }

    @Override
    public int getCount() {
        return grupos.size();
    }

    @Override
    public Grupo getItem(int position) {
        return grupos.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    // para poder modificar desde afuera
    public ArrayList<Grupo> getGrupos() {
        return this.grupos;
    }

    public void setGrupos(ArrayList<Grupo> grupos){
        this.grupos = grupos;
    }

    public void addGrupo(Grupo grupo){
        grupos.add(grupo);
    }

    public void setSeleccionados(ArrayList<Boolean> seleccionados){
        this.seleccionados = seleccionados;
    }

    public void addSeleccionados(Boolean seleccionado){
        seleccionados.add(seleccionado);
    }


    // patron de listview mejorado

    private class SimpleViewHolder {

        private final SparseArray<View> viewArray = new SparseArray<View>();
        private final View convertView;

        public SimpleViewHolder(View convertView) {
            this.convertView = convertView;
        }

        public View get(int id) {
            View view = this.viewArray.get(id, null);
            if(view == null) {
                view = this.convertView.findViewById(id);
                this.viewArray.put(id, view);
            }
            return view;
        }
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {

        inflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);

        View itemView = inflater.inflate(R.layout.lista_personalizada_grupos, parent, false);

        LinearLayout item_sel = (LinearLayout) itemView.findViewById(R.id.single_post_circuito_linearbox_grupos);
        TextView tvNombreGrupo = (TextView) itemView.findViewById(R.id.tv_nombre_grupo);
        TextView tvEstadoGrupo = (TextView) itemView.findViewById(R.id.tv_estado_grupo);
        ImageView ivImagenGrupo = (ImageView) itemView.findViewById(R.id.iv_imagen_grupo);

        Grupo grupo = getItem(position);
        String estado = "EN SEGUIMIENTO";
        int imagen = R.drawable.defaultuser;

        if(seleccionados.get(position))
        {
            item_sel.setBackgroundColor(Color.parseColor("#81F781"));
        }

        tvNombreGrupo.setText(grupo.getNombre());
        tvEstadoGrupo.setText(estado);
        ivImagenGrupo.setImageResource(imagen);

        return itemView;
    }
}

