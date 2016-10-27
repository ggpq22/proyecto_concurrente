package com.example.admin.pruebatracking.IU;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;

import com.example.admin.pruebatracking.R;


public class FragmentChats extends Fragment {



    ListViewAdapter adapter;

    String[] titulos;
    int[] imagenes;
    String[] descripciones;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View rootView = inflater.inflate(R.layout.fragment_chats, container, false);

        ListView lista = (ListView) rootView.findViewById(R.id.listView_listarChats);

        titulos = new String[10];
        imagenes = new int[10];
        descripciones = new String[10];

        for(int i = 0; i < 10; i++){
            titulos[i] = "Nombre del grupo";
            descripciones[i] = "Nombre del grupo: acá va la conversacion del grupo referido, esto es relleno";
            imagenes[i] = R.drawable.defaultuser;

        }

        adapter = new ListViewAdapter(getContext(), titulos, imagenes, descripciones);
        lista.setAdapter(adapter);

        lista.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Intent i = new Intent(getActivity(), ListarUnChat.class);
                i.putExtra("position", position);
                startActivity(i);


            }
        });
        return rootView;
    }
    public class ListViewAdapter extends BaseAdapter {

        Context context;
        String[] titulos;
        int[] imagenes;
        String[] descripciones;

        LayoutInflater inflater;

        public ListViewAdapter(Context context, String[] titulos, int[] imagenes, String[] descripciones) {
            this.context = context;
            this.titulos = titulos;
            this.imagenes = imagenes;
            this.descripciones = descripciones;
        }

        @Override
        public int getCount() {
            return titulos.length;
        }

        @Override
        public Object getItem(int position) {
            return null;
        }

        @Override
        public long getItemId(int position) {
            return 0;
        }

        public View getView(int position, View convertView, ViewGroup parent) {

            TextView tvTitulo;
            ImageView ivImagen;
            TextView tvEstado;
            TextView tvDescripcion;

            inflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);

            View itemView = inflater.inflate(R.layout.lista_personalizada_chats, parent, false);

            ivImagen = (ImageView) itemView.findViewById(R.id.imagen_lista_personalizada_chat);
            tvTitulo = (TextView) itemView.findViewById(R.id.tv_titulo_chat);
            tvDescripcion = (TextView) itemView.findViewById(R.id.tv_descripcion_chat);
            ivImagen.setImageResource(imagenes[position]);
            tvTitulo.setText(titulos[position]);
            tvDescripcion.setText(descripciones[position]);

            return itemView;
        }
    }
}