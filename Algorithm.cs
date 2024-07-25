using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Алгоритм_Мальгранжа
{
    internal class Algorithm
    {
        string[,] _matrix;
        GraphData[] _Tplus;
        GraphData[] _Tminus;
        List<string> _vertices;
        List<int> _subgraph;
        string t;
        public Algorithm(string[,] allComboBox) //конструктор
        {
            _vertices = new List<string>();
            _matrix = allComboBox;
            _Tplus = new GraphData[_matrix.GetLength(0)];
            _Tminus = new GraphData[_matrix.GetLength(0)];
            _subgraph = new List<int>();
            for(int i = 0; i < _matrix.GetLength(0); i++)
            {
                _Tplus[i] = new GraphData();
                _Tminus[i] = new GraphData();
            }
        }
        public string[,] matrix { get {return _matrix; } set { _matrix = value; } }
        public string tt { get { return t; } set { t = value; } }
        public GraphData[] Tplus { get { return _Tplus; } set { _Tplus = value; } }
        public GraphData[] Tminus { get { return _Tminus; } set { _Tminus = value; } }
        public List<string> Vertices { get { return _vertices; } set { _vertices = value; } }
        public void BeginAlgorithm()
        {
            while(_subgraph.Count != _matrix.GetLength(0))
            {
                int k = 0;
                while (_subgraph.Contains(k)) k++;
                DirectTransitiveClosure(k);
                InverseTransitiveClosure(k);
                Conjunction();
                if(t is null)
                {
                    for (int i = 0; i < _matrix.GetLength(0); i++)
                    {
                        if(_Tplus[i].Count != null)
                        {
                            t = t + i + ' ';
                        }
                        
                    }
                }
               
                for (int i = 0; i < _matrix.GetLength(0); i++)
                {
                    _Tplus[i] = new GraphData();
                    _Tminus[i] = new GraphData();
                }
            }
        }
        private void DirectTransitiveClosure(int i) //прямое транзитивное замыкание
        {
            if (_Tplus[i].Count == null) _Tplus[i].Count = "0";
            for (int j = 0; j < _Tplus.Length; j++)
            {
                if (!_subgraph.Contains(j) && _matrix[i, j] == "1")
                {
                    if (_Tplus[j].Count == null)
                    {
                        _Tplus[j].Count = (Int32.Parse(_Tplus[i].Count) + 1).ToString();
                        _Tplus[i].Connectedvertices.Add(j);
                    }
                }
            }
            for (int k = 0; k < _Tplus[i].Connectedvertices.Count(); k++)
            {
                DirectTransitiveClosure(Int32.Parse(_Tplus[i].Connectedvertices[k].ToString()));
            }
            
        }
        private void InverseTransitiveClosure(int i) //обратное транзитивное замыкание
        {
            if (_Tminus[i].Count == null) _Tminus[i].Count = "0";
            for (int j = 0; j < _Tminus.Length; j++)
            {
                if (!_subgraph.Contains(j) && _matrix[j, i] == "1")
                {
                    if (_Tminus[j].Count == null)
                    {
                        _Tminus[j].Count = (Int32.Parse(_Tminus[i].Count) + 1).ToString();
                        _Tminus[i].Connectedvertices.Add(j);
                    }
                }
            }
            for (int k = 0; k < _Tminus[i].Connectedvertices.Count(); k++)
            {
                InverseTransitiveClosure(Int32.Parse(_Tminus[i].Connectedvertices[k].ToString()));
            }
        }
        private void Conjunction()
        {
            string vertice = "{ ";
            for (int i = 0; i < _Tplus.Length; i++)
            {
                if (_Tplus[i].Count != null && _Tminus[i].Count != null)
                {
                    vertice += 'x' + (i + 1).ToString() + ", ";
                    _subgraph.Add(i);
                }
            }
            vertice = vertice.Substring(0, vertice.Length - 2);
            vertice +=" }";
            _vertices.Add(vertice);
        }
        public override string ToString()
        {
            string print = "Сильно связные подграфы заданного графа:\n";
            for (int i = 0; i < _vertices.Count; i++)
            {
                print += $"G{i+1} = <X{i+1}, A{i+1}>, X{i+1} = "+_vertices[i] + '\n';
            }
            return print;
        }
    }
}
