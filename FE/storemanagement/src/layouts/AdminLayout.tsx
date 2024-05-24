import React, { useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
import { Outlet } from 'react-router-dom';
import Avatar from 'access/images/headeravatar.png';
import Logo from 'access/images/store-logo.png'
import axios from '../common/baseAxios';
import {
    PieChartOutlined,
    LogoutOutlined,
    WalletOutlined,
    FileAddOutlined,
    ReadOutlined,
    UserOutlined,
    TeamOutlined,
    CalendarOutlined,
    HomeOutlined,
    DashboardOutlined,
    AppstoreAddOutlined,
    UserSwitchOutlined
} from '@ant-design/icons';
import { Breadcrumb, Layout, Menu, theme } from 'antd';
import { Cookies } from "react-cookie";
const { Header, Content, Footer, Sider } = Layout;
const AdminLayout = () => {
    const navigate = useNavigate();
    const {
        token: { colorBgContainer },
    } = theme.useToken();
    useEffect(() => {
        let cookie = new Cookies();
        const jwtToken = cookie.get(`token`);
        
        if (jwtToken !== null) {
            axios.defaults.headers.common['Authorization'] = `Bearer ${jwtToken}`;
        }
    }, [0]);
    return (
        <Layout style={{ minHeight: '100vh' }} >
            <Sider>
                <div className="demo-logo-vertical">
                    <img width={130} className={'logo'} src={Logo} alt="" />
                </div>
                <Menu theme="dark" defaultSelectedKeys={['1']} mode="inline">
                    <Menu.Item key={1} onClick={() => navigate("/")}><DashboardOutlined /> <span>Dashboard</span></Menu.Item>
                    <Menu.Item key={2} onClick={() => navigate("/tenant-management")}><AppstoreAddOutlined /> <span>Quản Lý Cửa Hàng</span></Menu.Item>
                    <Menu.Item key={3} onClick={() => navigate("/account-management")}><UserSwitchOutlined /> <span>Quản Lý Tài Khoản</span></Menu.Item>
                    <Menu.Item key={4} onClick={() => navigate("/category-management")}><UserSwitchOutlined /> <span>Quản Lý Thể Loại</span></Menu.Item>
                    <Menu.Item key={5} onClick={() => navigate("/product-management")}><UserSwitchOutlined /> <span>Quản Lý Sản Phẩm</span></Menu.Item>
                    <Menu.Item key={6} onClick={() => navigate("/sell-management")}><UserSwitchOutlined /> <span>Tạo Đơn Hàng</span></Menu.Item>
                    <Menu.Item key={7} onClick={() => navigate("/order-management")}><UserSwitchOutlined /> <span>Quản Lý Đơn Hàng</span></Menu.Item>
                    <Menu.Item key={8} onClick={() => navigate("/login")}><LogoutOutlined /> <span>Đăng Xuất</span></Menu.Item>
                </Menu>
            </Sider>
            <Layout>
                <Header style={{ padding: 0, background: colorBgContainer, textAlign: 'right' }} >
                    <img src={Avatar} alt='' style={{ paddingRight: `17px`, width: `65px`, cursor: `pointer` }} />
                </Header>
                <Content style={{ margin: '16px 16px' }}>
                    <div style={{ padding: 24, minHeight: 360, background: colorBgContainer }}><Outlet /></div>
                </Content>
                <Footer style={{ textAlign: 'center' }}>Dinh Kien ©2024 Created by Dinh Kien</Footer>
            </Layout>
        </Layout>
    );
};

export default AdminLayout;
