using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using ControleFacil.Api.Contract.Usuario;
using ControleFacil.Api.Domain.Models;
using ControleFacil.Api.Domain.Repository.Interfaces;
using ControleFacil.Api.Domain.Services.Interfaces;
using ControleFacil.Api.Exceptions;

namespace ControleFacil.Api.Domain.Services.Classes
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        private readonly TokenService _tokenService;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, TokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<UsuarioLoginResponseContract> Autenticar(UsuarioLoginRequestContract usuarioLoginRequest)
        {
            UsuarioResponseContract usuario = await ObterEmail(usuarioLoginRequest.Email);
            var hashSenha = GerarHashSenha(usuarioLoginRequest.Senha);
            if (usuario is null || usuario.Senha != hashSenha)
            {
                throw new AuthenticationException($"Usuario ou senha iválida.");
            }

            return new UsuarioLoginResponseContract
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Token = _tokenService.GerarToken(_mapper.Map<Usuario>(usuario))
            };
        }

        public async Task<UsuarioResponseContract> Adicionar(UsuarioRequestContract entidade, long idUsuario)
        {
            var usuario = _mapper.Map<Usuario>(entidade);

            usuario.Senha = GerarHashSenha(usuario.Senha);
            usuario.DataCadastro = DateTime.Now;

            usuario = await _usuarioRepository.Adicionar(usuario);

            return _mapper.Map<UsuarioResponseContract>(usuario);

        }

        public async Task<UsuarioResponseContract> Atualizar(long id, UsuarioRequestContract entidade, long idUsuario)
        {
            _ = await ObterTodos(id) ?? throw new NotFoundException("Usuário não encontrado para atualização.");

            var usuario = _mapper.Map<Usuario>(entidade);
            usuario.Id = id;
            usuario.Senha = GerarHashSenha(entidade.Senha);

            usuario = await _usuarioRepository.Atualizar(usuario);

            return _mapper.Map<UsuarioResponseContract>(usuario);

        }

        public async Task Inativar(long id, long idUsuario)
        {
            var usuario = await _usuarioRepository.ObterPorId(id) ?? throw new NotFoundException("Usuário não encontrado para inativação.");
            await _usuarioRepository.Deletar(_mapper.Map<Usuario>(usuario));
        }

        public async Task<IEnumerable<UsuarioResponseContract>> ObterTodos(long idUsuario)
        {
            var usuarios = await _usuarioRepository.ObterTodos();
            return usuarios.Select(usuario => _mapper.Map<UsuarioResponseContract>(usuario));
        }

        public async Task<UsuarioResponseContract> ObterPorId(long id, long idUsuario)
        {
            var usuario = await _usuarioRepository.ObterPorId(id);
            return _mapper.Map<UsuarioResponseContract>(usuario);
        }

        public async Task<UsuarioResponseContract> ObterEmail(string email)
        {
            var usuario = await _usuarioRepository.Obter(email);
            return _mapper.Map<UsuarioResponseContract>(usuario);
        }

        private static string GerarHashSenha(string senha)
        {
            string hashSenha;
            byte[] bytesSenha = Encoding.UTF8.GetBytes(senha);
            byte[] byteHashSenha = SHA256.HashData(bytesSenha);
            hashSenha = BitConverter.ToString(byteHashSenha).Replace("-", "").Replace("-", "").ToLower();

            return hashSenha;
        }


    }
}