import React from 'react';
import { Col, Row, Form, Input, Button, message } from 'antd';
import Store from 'access/images/store.jpg';
import { PageWrapper } from 'pages/Login/Login.styled';
import { useNavigate } from "react-router-dom";
import LoginDto from './models/LoginDto';
import axios from '../../common/baseAxios';
import { Cookies } from "react-cookie";
let cookie = new Cookies();
const Login = () => {
    const navigate = useNavigate();
    const onFinish = (input: LoginDto) => {
        axios.post(`Auth/Login`, input).then((result: any) => {
            let { data } = result;
            if(data?.success) {
                let expires = new Date();
                expires.setTime(expires.getTime() + 604800);
                cookie.set('token', data.data.token, { path: "/", expires: expires });
                axios.defaults.headers.common = { Authorization: `Bearer ${data.data.token}` };
                navigate("/");
                message.success('Đăng nhập thành công.')
            }
            else {
                message.error('Đăng nhập thất bại.')
            }
        })
    }
    return <PageWrapper>
        <Row gutter={[0, 0]} className={'row-1'}>
            <Col span={12} className={'box-left'}>
                <img className={'bg-1'} src={Store} alt="" />
            </Col>
            <Col span={10} className={'content-login'}>
                <div>
                    <h2 className='title-login'>ĐĂNG NHẬP</h2>
                    <Form
                        name="basic"
                        layout={'vertical'}
                        style={{ maxWidth: 600 }}
                        onFinish={onFinish}
                        autoComplete="off"
                    >
                        <Form.Item
                            label="Tên đăng nhập(E-mail):"
                            name="email"
                            rules={[{ required: true, message: 'Vui lòng nhập tên đăng nhập của bạn!' }]}
                        >
                            <Input placeholder='Tên đăng nhập của bạn' />
                        </Form.Item>

                        <Form.Item
                            label="Mật khẩu"
                            name="password"
                            rules={[{ required: true, message: 'Vui lòng nhập mật khẩu của bạn!' }]}
                        >
                            <Input.Password placeholder='Mật khẩu của bạn' />
                        </Form.Item>

                        <Form.Item wrapperCol={{ span: 24 }}>
                            <Button type="primary" htmlType="submit" style={{ width: '100%' }}>
                                ĐĂNG NHẬP
                            </Button>
                        </Form.Item>
                    </Form>
                </div>
            </Col>
        </Row>
    </PageWrapper>
}
export default Login;